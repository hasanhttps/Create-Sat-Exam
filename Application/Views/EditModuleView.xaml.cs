using System;
using Application.ViewModels;
using System.Windows.Controls;
using Database.Entities.Concretes;

namespace Application.Views {
    public partial class EditModuleView : Page {
        public EditModuleView(Exam exam) {
            InitializeComponent();
            DataContext = new EditModuleViewModel(exam);
        }
    }
}
