using System;
using System.Windows;
using System.Threading;
using Application.Commands;
using System.Windows.Input;
using System.ComponentModel;
using Database.Entities.Concretes;
using System.Runtime.CompilerServices;
using static Application.Models.DatabaseNamespace.Database;

namespace Application.ViewModels;

public class CreateExamViewModel : INotifyPropertyChanged {

    // Private Fields

    private string? _quizName = null;
    private DateTime? _quizActivationDate = null;
    private DateTime? _quizActivationTime = null;


    // Binding Properties

    public string? QuizName { get => _quizName;
        set {
            _quizName = value;
            OnPropertyChanged();
        }
    }
    public DateTime? QuizActivationDate { get => _quizActivationDate;
        set {
            _quizActivationDate = value;
            OnPropertyChanged();
        }
    }
    public DateTime? QuizActivationTime { get => _quizActivationTime;
        set {
            _quizActivationTime = value;
            OnPropertyChanged();
        }
    }
    public ICommand? CreateButtonCommand { get; set; }

    // Constructor

    public CreateExamViewModel() { 
        
        SetCommands();
    }

    // Functions

    private void SetCommands() {

        CreateButtonCommand = new RelayCommand(Create);
    }

    private void Create(object? param) {

        if (QuizActivationDate == null || QuizActivationTime == null || QuizName == null) {
            MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Exam exam = new Exam() {
            ActivationDate = new DateTime(QuizActivationDate.Value.Year, QuizActivationDate.Value.Month, QuizActivationDate.Value.Day, QuizActivationTime.Value.Hour, QuizActivationTime.Value.Minute, 0),
            Name = QuizName
        };

        Thread addExam = new Thread(() => {

            DbContext.Exams.Add(exam);
            DbContext.SaveChanges();
        });
        addExam.Start();
    }

    // INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}