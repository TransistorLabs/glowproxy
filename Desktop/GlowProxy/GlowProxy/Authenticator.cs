using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

using Hardcodet.Wpf.TaskbarNotification;

namespace GlowProxy
{
    public class Authenticator
    {
        private const string AuthCookieName = ".AspNet.ApplicationCookie";

        public Cookie Cookie { get; set; }

        public bool Authenticate()
        {
            var serverhost = GlowProxy.Properties.Settings.Default.Server;
            var username = GlowProxy.Properties.Settings.Default.Username;
            var password = GlowProxy.Properties.Settings.Default.Password;

            var baseUri = new Uri("http://" + serverhost);
            
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookieContainer;

            var client = new HttpClient(handler);

            client.BaseAddress = baseUri;
            var content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password) 
            });
 
            var result = client.PostAsync("/Account/RemoteLogin", content).Result;
            var resultContent = result.Content.ReadAsStringAsync().Result;

            var cookies = cookieContainer.GetCookies(baseUri);
            Cookie = cookies[AuthCookieName];

            var success = Cookie != null;

            if (success)
            {
                Core.ApplicationIcon.ShowBalloonTip("Success!", "Authenticated to server.", BalloonIcon.Info);
            }
            else
            {
                Core.ApplicationIcon.ShowBalloonTip("Login failed.", "Failed to login.", BalloonIcon.Error);
                Core.ShowSettingsWindow();
            }

            return success;
        }
    }
}
