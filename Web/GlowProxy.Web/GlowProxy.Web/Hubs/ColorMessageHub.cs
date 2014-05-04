using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GlowProxy.Web.Hubs
{
    [Authorize]
    public class ColorMessageHub : Hub
    {
        public void SendColorIndex(string userId, string colorIndex)
        {
            Clients.User(userId).receiveColorIndex(colorIndex);
        }

        public void SendPing(string userId)
        {
            Clients.User(userId).receivePing();
        }
    }
}