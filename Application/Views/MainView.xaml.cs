using System;
using System.Windows;
using Application.ViewModels;

namespace Application.Views {
    public partial class MainView : Window {
        public MainView() {

            InitializeComponent();
            DataContext = new MainViewModel(CreateExamFrame, ExamsFrame, EditExamFrame, StatisticsFrame);
        }
    }
}
