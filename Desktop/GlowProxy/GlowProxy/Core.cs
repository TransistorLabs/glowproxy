using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Windows;

using Hardcodet.Wpf.TaskbarNotification;

namespace GlowProxy
{
    public static class Core
    {
        private static Settings _settingsWindow;
        private static Authenticator _authenticator;
        private static ColorMessageClient _colorMessageClient;
        private static TaskbarIcon _tb;

        public static void ShowSettingsWindow()
        {
            var window = Application.Current.Dispatcher.Invoke(
                () => new Settings());
            window.Dispatcher.Invoke(window.Show);
        }

        public static Authenticator Authenticator
        {
            get { return _authenticator ?? (_authenticator = new Authenticator()); }
        }

        public static ColorMessageClient ColorMessageClient
        {
            get { return _colorMessageClient ?? (_colorMessageClient = new ColorMessageClient()); }
        }

        public static TaskbarIcon ApplicationIcon
        {
            get
            { return _tb; }
            set { _tb = value; }
        }
    }
}
