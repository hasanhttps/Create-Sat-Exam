using System;
using Application.Views;
using System.Windows.Input; 
using Application.Commands;
using System.Windows.Controls;
using Database.Entities.Concretes;
using System.Collections.ObjectModel;
using static Application.Models.DatabaseNamespace.Database;

namespace Application.ViewModels;

public class StatisticsViewModel {

    // Private Fields

    private Frame StatisticsFrame;

    // Binding Properties

    public ICommand? SelectionChangedCommand { get; set; }
    public ObservableCollection<ExamResult> ExamResults { get; set; } = new();

    // Constructor

    public StatisticsViewModel(Frame statisticsFrame) {

        StatisticsFrame = statisticsFrame;
        SetCommands();
        foreach (var item in DbContext.ExamResults) {
            ExamResults!.Add(item);
        }
    }

    // Functions

    private void SelectionChanged(object? param) {

        ExamResult? examResult = param as ExamResult;

        if (examResult != null) {
            StatisticsFrame.Content = new StudentsAnswersPage(examResult);   
        }
    }

    private void SetCommands() {

        SelectionChangedCommand = new RelayCommand(SelectionChanged);
    }
}