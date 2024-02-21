using System;
using System.Threading;
using Application.Views;
using Database.DbContexts;
using System.Windows.Controls;
using static Application.Models.DatabaseNamespace.Database;

namespace Application.ViewModels;

public class MainViewModel {

    // Constructor

    public MainViewModel(Frame CreateExamFrame, Frame ExamsFrame, Frame EditExamFrame, Frame StatisticsFrame) {

        CreateExamFrame.Content = new CreateExamView();
        ExamsFrame.Content = new ExamsView(ExamsFrame);
        EditExamFrame.Content = new ExamsView(EditExamFrame, "edit");
        StatisticsFrame.Content = new StatisticsView(StatisticsFrame);
    }
}
