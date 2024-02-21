using System;
using System.Linq;
using System.Windows;
using Application.Views;
using Database.DbContexts;
using Application.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Database.Entities.Concretes;
using System.Collections.ObjectModel;
using static Application.Models.DatabaseNamespace.Database;

namespace Application.ViewModels;

public class ExamsViewModel {

    // Private Fields

    private string _operation;
    private Frame MainFrame;

    // Binding Properties

    public ICommand SelectionChangedCommand { get; set; }
    public ObservableCollection<Exam> Exams { get; set; } = new();

    // Constructor

    public ExamsViewModel(Frame frame, string? operation = "create") { 
        
        MainFrame = frame;
        _operation = operation;

        SelectionChangedCommand = new RelayCommand(SelectionChanged);
        try {
            DbContext = new SatExaminationDbContext();
            foreach (var exam in DbContext.Exams.ToList()) {
                Exams.Add(exam);
            }
        }
        catch(Exception ex) {
            MessageBox.Show(ex.Message, "Error 1", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // Functions
    
    private void SelectionChanged(object? param) {
        Exam? exam = (param as Exam);
        if (exam != null) {
            if (_operation == "create")
                MainFrame.Content = new CreateModuleView(MainFrame, exam);
            else
                MainFrame.Content = new EditModuleView(exam);
        }
    }
}