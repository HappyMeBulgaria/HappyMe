using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Te4Fest.Web.Startup))]
namespace Te4Fest.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
