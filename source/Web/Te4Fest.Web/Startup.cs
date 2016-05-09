using Microsoft.Owin;

[assembly: OwinStartup(typeof(Te4Fest.Web.Startup))]
namespace Te4Fest.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
