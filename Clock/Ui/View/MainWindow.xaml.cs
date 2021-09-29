using System.Windows;
using Clock.Ui.ViewModel;
using MahApps.Metro.Controls;

namespace Clock.Ui.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the main window was loaded
        /// </summary>
        /// <param name="sender">The <see cref="MainWindow"/></param>
        /// <param name="e">The event arguments</param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            SetBlackoutDates();

            if (DataContext is MainWindowViewModel viewModel)
                viewModel.InitViewModel(SetBlackoutDates);
        }

        /// <summary>
        /// Sets the blackout dates
        /// </summary>
        private void SetBlackoutDates()
        {
            CalendarView.BlackoutDates.AddDatesInPast();
        }
    }
}
