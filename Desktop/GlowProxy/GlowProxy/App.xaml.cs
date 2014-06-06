using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
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
        private static Timer _sendTimer;
        private bool _pingActivated = false;
        private Color _color = new Color();
        private Color _lastColor = new Color();

        private void InitApplication()
        {
            //initialize NotifyIcon
            Core.ApplicationIcon = (TaskbarIcon)FindResource("GlowProxyNotifyIcon");
            Core.Device.Open();

            // Configure inputs from device
            Core.Device.OnClick = () =>
            {
                _pingActivated = true;
            };

            Core.Device.OnColorChange = (color) =>
            {
                _color = color;   
            };

            // Configure outputs to device
            Core.ColorMessageClient.OnPing = () => Core.Device.BlinkWhite(4);
            Core.ColorMessageClient.OnRemoteColorIndexChange = (colorString) => Core.Device.SetRemoteColor(new Color(colorString));
            
            _sendTimer = new Timer(50);
            _sendTimer.Elapsed += sendTimer_Elapsed;
            _sendTimer.Start();
            
            Core.Device.StartProcessingInput();


        }

        void sendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _sendTimer.Stop();
            if (_pingActivated)
            {
                if (Core.ColorMessageClient.Connected)
                {
                    Core.ColorMessageClient.SendPing();
                }
                _pingActivated = false;
            }

            if (_color.ToString() != _lastColor.ToString())
            {
                if (Core.ColorMessageClient.Connected)
                {
                    Core.ColorMessageClient.SendColorIndex(_color.ToString());
                }
                _lastColor = _color;
            }
            _sendTimer.Start();
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
