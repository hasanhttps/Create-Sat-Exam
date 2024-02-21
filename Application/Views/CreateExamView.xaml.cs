using System;
using Application.ViewModels;
using System.Windows.Controls;


namespace Application.Views {
    public partial class CreateExamView : Page {
        public CreateExamView() {

            DataContext = new CreateExamViewModel();
            InitializeComponent();
        }
    }
}
