﻿using System;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tb = (TaskbarIcon)FindResource("GlowProxyNotifyIcon");
            tb.ShowBalloonTip("Connecting...", "Now attempting to connect to server.", BalloonIcon.Info);
            this.Close();
        }
    }
}