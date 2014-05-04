using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
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

        private void InitApplication()
        {
            //initialize NotifyIcon
            Core.ApplicationIcon = (TaskbarIcon)FindResource("GlowProxyNotifyIcon");
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
            Core.ShowSettingsWindow();
        }

        private void App_OnDoubleClick(object sender, RoutedEventArgs e)
        {
            Core.ShowSettingsWindow();
        }
    }
}
