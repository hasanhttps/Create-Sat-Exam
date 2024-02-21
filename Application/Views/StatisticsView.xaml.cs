using System;
using Application.ViewModels;
using System.Windows.Controls;

namespace Application.Views {
    public partial class StatisticsView : Page {
        public StatisticsView(Frame frame) {

            DataContext = new StatisticsViewModel(frame);
            InitializeComponent();
        }
    }
}
