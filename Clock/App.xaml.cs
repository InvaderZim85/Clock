using System.Windows;
using Clock.Ui.View;

namespace Clock
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Contains the main window
        /// </summary>
        private MainWindow _mainWindow;

        /// <summary>
        /// Occurs when the application starts
        /// </summary>
        /// <param name="sender">The <see cref="App"/></param>
        /// <param name="e">The event arguments</param>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            _mainWindow = new MainWindow();

            var settings = Clock.Properties.Settings.Default;
            if (settings.PosX == 0 || settings.PosY == 0)
                _mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
            {
                _mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                _mainWindow.Left = settings.PosX;
                _mainWindow.Top = settings.PosY;
            }

            _mainWindow.Show();
        }

        /// <summary>
        /// Occurs when the application will closed
        /// </summary>
        /// <param name="sender">The <see cref="App"/></param>
        /// <param name="e">The event arguments</param>
        private void App_OnExit(object sender, ExitEventArgs e)
        {
            Clock.Properties.Settings.Default.PosX = _mainWindow?.Left ?? 0;
            Clock.Properties.Settings.Default.PosY = _mainWindow?.Top ?? 0;
            Clock.Properties.Settings.Default.Save();
        }
    }
}
