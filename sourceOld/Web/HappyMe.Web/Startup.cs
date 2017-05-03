using Microsoft.Owin;

[assembly: OwinStartup(typeof(HappyMe.Web.Startup))]

namespace HappyMe.Web
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
