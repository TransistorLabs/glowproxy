using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(GlowProxy.Web.Startup))]
namespace GlowProxy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        
        }
    }
}
