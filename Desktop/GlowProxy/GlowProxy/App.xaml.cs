using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Hardcodet.Wpf.TaskbarNotification;

namespace GlowProxy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon tb;

        private void InitApplication()
        {
            //initialize NotifyIcon
            tb = (TaskbarIcon)FindResource("GlowProxyNotifyIcon");
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            InitApplication();
        }

        private void ContextMenuQuit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Shutdown();
        }

        private void ContextMenuSettings_OnClick(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new Settings();
            settingsWindow.Show();
        }
    }
}
