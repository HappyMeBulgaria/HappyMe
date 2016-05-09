using Microsoft.Owin;

[assembly: OwinStartup(typeof(Te4Fest.Web.Api.Startup))]

namespace Te4Fest.Web.Api
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
