using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

using GlowProxyAPI.Net;

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
            Core.Device.Open();
            Core.Device.StartProcessingInput();
            Core.Device.OnClick = () =>
            {
                if (Core.ColorMessageClient.Connected)
                {
                    Core.ColorMessageClient.SendPing();
                }
            };

            Core.Device.OnColorChange = (color) =>
            {
                if (Core.ColorMessageClient.Connected)
                {
                    Core.ColorMessageClient.SendColorIndex(color.ToString());
                }
            };

            Core.ColorMessageClient.OnPing = () => Core.Device.BlinkWhite(4);
            Core.ColorMessageClient.OnRemoteColorIndexChange = (colorString) => Core.Device.SetRemoteColor(new Color(colorString));

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
