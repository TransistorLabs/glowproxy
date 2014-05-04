using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Hardcodet.Wpf.TaskbarNotification;

using Microsoft.AspNet.SignalR.Client;

namespace GlowProxy
{
    public class ColorMessageClient
    {
        private HubConnection _hubConnection;
        private IHubProxy _proxy;

        public bool StartClient()
        {
            var cookieContainer = new CookieContainer();
            var cookie = Core.Authenticator.Cookie;
            var success = true;

            if (cookie == null)
            {
                //Attempt authentication to see if we can get a cookie
                success = Core.Authenticator.Authenticate();
            }
            if (success && cookie != null)
            {
                if (_hubConnection != null)
                {
                    _hubConnection.Stop();
                    _hubConnection.Dispose();
                }

                cookieContainer.Add(cookie);
                var url = "http://" + Properties.Settings.Default.Server + "";
                _hubConnection = new HubConnection(url);
                _hubConnection.CookieContainer = cookieContainer;
               
                _proxy = _hubConnection.CreateHubProxy("ColorMessageHub");
                _proxy.On<string>("receiveColorIndex", index => MessageBox.Show(index));
                _proxy.On("receivePing", () =>
                {
                    MessageBox.Show("pING!");
                    Debugger.Log(0,"message", "ping");
                });
                _hubConnection.Start().Wait();
                Core.ApplicationIcon.ShowBalloonTip("Running!", "Successfully started client service.", BalloonIcon.Info);
                return true;
            }

            Core.ApplicationIcon.ShowBalloonTip("Failed!", "Unable to start client service.", BalloonIcon.Error);
            return false;
        }

        public void SendPing()
        {
            _proxy.Invoke("sendPing", GlowProxy.Properties.Settings.Default.PairedUsername);
        }

        public void SendColorIndex(string colorIndex)
        {
            _proxy.Invoke("sendColorIndex", GlowProxy.Properties.Settings.Default.PairedUsername,
                colorIndex
                );
        }

        public void StopClient()
        {
            _hubConnection.Stop();
        }
    }
}
