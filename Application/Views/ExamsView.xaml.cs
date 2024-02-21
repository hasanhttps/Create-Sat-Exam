using System;
using Application.ViewModels;
using System.Windows.Controls;

namespace Application.Views {
    public partial class ExamsView : Page {
        public ExamsView(Frame ExamsFrame, string? operation = "create") {

            DataContext = new ExamsViewModel(ExamsFrame, operation);
            InitializeComponent();
        }
    }
}
