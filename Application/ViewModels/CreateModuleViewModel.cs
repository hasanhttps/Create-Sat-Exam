using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Application.Views;
using Application.Commands;
using System.Windows.Input;
using System.ComponentModel; 
using System.Windows.Controls;
using System.Collections.Generic;
using Database.Entities.Concretes;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using static Application.Models.DatabaseNamespace.Database;

namespace Application.ViewModels;

public class CreateModuleViewModel : INotifyPropertyChanged{

    // Private Fields
    
    private Exam Exam;
    private Frame MainFrame;
    private string openEndedAnswerText;
    private Module _currentModule = new();
    private int CurrentQuestionNumber = 0;

    // Binding Properties

    public string OpenEndedAnswerText { get => openEndedAnswerText; 
        set {
            openEndedAnswerText = value;
            OnPropertyChanged();
        }
    }
    public Module CurrentModule { get => _currentModule;
        set {
            _currentModule = value;
            OnPropertyChanged();
        } 
    }
    public ICommand? ImageAddCommand { get; set; }
    public ICommand? NextButtonCommand { get; set; }
    public ICommand? SubmitButtonCommand { get; set; }
    public ICommand? CreateQuestionCommand { get; set; }
    public ICommand? CreateOpenEndedQuestionCommand { get; set; }
    public ObservableCollection<Question> Questions { get; set; } = new();

    // Constructor

    public CreateModuleViewModel(Frame frame, Exam exam) {

        Exam = exam;
        MainFrame = frame;

        CreateQuestion(null);
        SetCommands();
    }

    // Functions

    private void SetCommands() {

        ImageAddCommand = new RelayCommand(AddImage);
        SubmitButtonCommand = new RelayCommand(Submit);
        NextButtonCommand = new RelayCommand(NextModule);
        CreateQuestionCommand = new RelayCommand(CreateQuestion);
        CreateOpenEndedQuestionCommand = new RelayCommand(CreateOpenEndedQuestion);
    }

    private void NextModule(object? param) {

        if (Exam.Modules != null) {
            if(Exam.Modules.Count != 4)
                MainFrame.Content = new CreateModuleView(MainFrame, Exam);
            if (Exam.Modules.Count == 4)
                MainFrame.Content = new ExamsView(MainFrame);
        }
    }

    private void CreateQuestion(object? param) {

        List<string> Variants = new List<string>() { "A", "B", "C", "D" };
        Question newQuestion = new Question();
        newQuestion.QuestionNumber = ++CurrentQuestionNumber;
        newQuestion.Answers = new ObservableCollection<Answer>();
        newQuestion.isOpenEnded = false;
        for (int i = 0; i < 4; i++) {
            Answer newAnswer = new Answer();
            newAnswer.Variant = Variants[i];
            newAnswer.QuestionId = CurrentQuestionNumber;
            newQuestion.Answers.Add(newAnswer);
        }
        Questions.Add(newQuestion);
    }

    private void CreateOpenEndedQuestion(object? param) {

        Question newQuestion = new Question();
        newQuestion.QuestionNumber = ++CurrentQuestionNumber;
        newQuestion.Answers = new ObservableCollection<Answer>();
        newQuestion.isOpenEnded = true;

        Answer newAnswer = new Answer();
        newAnswer.isCorrect = true;
        newAnswer.isOpenEnded = true;
        newAnswer.QuestionId = CurrentQuestionNumber;
        newQuestion.Answers.Add(newAnswer);
        Questions.Add(newQuestion);
    }

    private void AddImage(object? param) {

        int questionNumber = Convert.ToInt32(param);

        OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
        if (fileDialog.ShowDialog() == true) {
            try {
                using (FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read)) {
                    using (BinaryReader br = new BinaryReader(fs)) {
                        foreach(var question in Questions)
                            if (question.QuestionNumber == questionNumber)
                                question.QuestionImage = br.ReadBytes((int)fs.Length);
                        MessageBox.Show("Question Image uploaded successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void Submit(object? param) {

        if (CurrentModule.Subject.Contains("Sat Verbal"))
            CurrentModule.Subject = "Sat Verbal";
        else
            CurrentModule.Subject = "Sat Math";

        if (CurrentModule.Subject == null || CurrentModule.ModuleNumber == null || CurrentModule.Time == null) {
            MessageBox.Show("Please fill all fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (CurrentModule.ModuleNumber != 1 && CurrentModule.ModuleNumber != 2) {
            MessageBox.Show("Module number can only be 1 or 2.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (Exam.Modules != null) {
            foreach (var module in Exam.Modules) {
                if (module.ModuleNumber == CurrentModule.ModuleNumber && module.Subject == CurrentModule.Subject) {
                    MessageBox.Show($"There is already a module with subject {module.Subject} and with module number {module.ModuleNumber}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }

        foreach (var question in Questions) {

            bool isCorrectHave = false;
            if (question.QuestionText == null || question.QuestionPoint == 0) {
                MessageBox.Show($"Please fill all question fields in question number {question.QuestionNumber}.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            foreach (var answer in question.Answers) {
                if (answer.AnswerText == null) {
                    MessageBox.Show($"Please fill answer fields in question number {question.QuestionNumber}.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (answer.isCorrect) isCorrectHave = true;
            }
            if (!isCorrectHave) {
                MessageBox.Show($"Please choose correct answer from question {question.QuestionNumber}.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        CurrentModule.ExamId = Exam.Id;
        DbContext.Modules.Add(CurrentModule);
        DbContext.SaveChanges();

        foreach(var question in Questions) {
            question.ModuleId = CurrentModule.Id;
            DbContext.Questions.Add(question);
            DbContext.SaveChanges();
        }
    }

    // INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}