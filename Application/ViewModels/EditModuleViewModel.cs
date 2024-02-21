using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Threading;
using Application.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Database.Entities.Concretes;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using static Application.Models.DatabaseNamespace.Database;

namespace Application.ViewModels;

public class EditModuleViewModel : INotifyPropertyChanged {

    // Privat Fields

    private int? time;
    private Exam Exam;
    private Frame MainFrame;
    private int? moduleNumber;
    private Module _currentModule;
    private int CurrentQuestionNumber;
    private string openEndedAnswerText;

    // Binding Properties

    public int? Time { get => time;
        set { 
            time = value;
            OnPropertyChanged();
        }
    }
    public int? ModuleNumber { get => moduleNumber;
        set { 
            moduleNumber = value; 
            OnPropertyChanged();
        }
    }
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
    public ICommand? EditButtonCommand { get; set; }
    public ICommand? CreateQuestionCommand { get; set; }
    public ICommand? CreateOpenEndedQuestionCommand { get; set; }
    public ObservableCollection<Question> Questions { get; set; } = new();

    // Constructor

    public EditModuleViewModel(Exam exam) {

        Exam = exam;

        CurrentModule = DbContext.Modules.Where(m => m.Subject == "Sat Verbal" && m.ModuleNumber == 1 && m.ExamId == exam.Id).First();
        ModuleNumber = CurrentModule.ModuleNumber;
        Time = CurrentModule.Time;

        Questions.Clear();
        foreach(var question in CurrentModule.Questions.ToList())
            Questions.Add(question);
        SetCommands();
    }

    // Functions

    private void SetCommands() {

        EditButtonCommand = new RelayCommand(Edit);
        ImageAddCommand = new RelayCommand(AddImage);
        NextButtonCommand = new RelayCommand(NextModule);
        CreateQuestionCommand = new RelayCommand(CreateQuestion);
        CreateOpenEndedQuestionCommand = new RelayCommand(CreateOpenEndedQuestion);
    }

    private void NextModule(object? param) {

        if (Exam.Modules != null) {
            if (CurrentModule.Subject == "Sat Verbal" && CurrentModule.ModuleNumber == 1) CurrentModule = Exam.Modules.Where(p => p.Subject == "Sat Verbal" && p.ModuleNumber == 2).First();
            else if (CurrentModule.Subject == "Sat Verbal" && CurrentModule.ModuleNumber == 2) CurrentModule = Exam.Modules.Where(p => p.Subject == "Sat Math" && p.ModuleNumber == 1).First();
            else if (CurrentModule.Subject == "Sat Math" && CurrentModule.ModuleNumber == 1) CurrentModule = Exam.Modules.Where(p => p.Subject == "Sat Math" && p.ModuleNumber == 2).First();
            else if (CurrentModule.Subject == "Sat Math" && CurrentModule.ModuleNumber == 2) return;

            ModuleNumber = CurrentModule.ModuleNumber;
            Time = CurrentModule.Time;
            Questions.Clear();
            foreach (var question in CurrentModule.Questions.ToList())
                Questions.Add(question);
        }
    }

    private void CreateQuestion(object? param) {

        Question newQuestion = new Question();
        newQuestion.QuestionNumber = ++CurrentQuestionNumber;
        newQuestion.Answers = new ObservableCollection<Answer>();
        newQuestion.isOpenEnded = false;
        for (int i = 0; i < 4; i++) {
            Answer newAnswer = new Answer();
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

    private void Edit(object? param) {

        Thread edit = new Thread(() => {

            DbContext.Modules.Update(CurrentModule);
            DbContext.SaveChanges();

            foreach(var question in Questions) {
                question.ModuleId = CurrentModule.Id;
                DbContext.Questions.Update(question);
                DbContext.SaveChanges();
            }
        });
        edit.Start();
    }

    // INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}