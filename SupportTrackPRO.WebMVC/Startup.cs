using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SupportTrackPRO.WebMVC.Startup))]
namespace SupportTrackPRO.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
