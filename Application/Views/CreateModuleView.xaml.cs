using System;
using Application.ViewModels;
using System.Windows.Controls;
using Database.Entities.Concretes;

namespace Application.Views {
    public partial class CreateModuleView : Page {
        public CreateModuleView(Frame frame, Exam exam) {
            DataContext = new CreateModuleViewModel(frame, exam);
            InitializeComponent();
        }
    }
}
