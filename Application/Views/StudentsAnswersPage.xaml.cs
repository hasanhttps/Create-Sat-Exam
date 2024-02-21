using System;
using Application.ViewModels;
using System.Windows.Controls;
using Database.Entities.Concretes;

namespace Application.Views {
    public partial class StudentsAnswersPage : Page {
        public StudentsAnswersPage(ExamResult examResult) {
            InitializeComponent();
            DataContext = new StudentsAnwersPageModel(examResult);
        }
    }
}
