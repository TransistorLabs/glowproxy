using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GlowProxy.Web.Hubs
{
    public class ColorMessageHub : Hub
    {
        public void Send(string connectionId, string message)
        {
            Clients.Client(connectionId).Send(message);
        }

        public void SendAll(string message)
        {
            Clients.All.renderTest(message);
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}