using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Hardcodet.Wpf.TaskbarNotification;

namespace GlowProxy
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            serverAddressTextBox.Text   = GlowProxy.Properties.Settings.Default.Server;
            userNameTextBox.Text        = GlowProxy.Properties.Settings.Default.Username;
            passwordTextBox.Password    = GlowProxy.Properties.Settings.Default.Password;
            pairedUsernameTextBox.Text  = GlowProxy.Properties.Settings.Default.PairedUsername;
        }

        private void SaveConnect_Click(object sender, RoutedEventArgs e)
        {
            Save(true);
        }

        private void SaveReconnectNoClose_Click(object sender, RoutedEventArgs e)
        {
            Save(false);
        }

        private void Save(bool exit)
        {
            GlowProxy.Properties.Settings.Default.Server = serverAddressTextBox.Text;
            GlowProxy.Properties.Settings.Default.Username = userNameTextBox.Text;
            GlowProxy.Properties.Settings.Default.Password = passwordTextBox.Password;
            GlowProxy.Properties.Settings.Default.PairedUsername = pairedUsernameTextBox.Text;

            GlowProxy.Properties.Settings.Default.Save();

            var success = Core.Authenticator.Authenticate();

            if (success)
            {
                if (Core.ColorMessageClient.StartClient())
                {
                    if (exit)
                    {
                        this.Close();
                    }
                }
            }
        }

        private void TestPing_Click(object sender, RoutedEventArgs e)
        {
            Core.ColorMessageClient.SendPing();
        }

        private void TestColorMessage_Click(object sender, RoutedEventArgs e)
        {
            Core.ColorMessageClient.SendColorIndex(testMessageTextBox.Text);
        }
    }
}
