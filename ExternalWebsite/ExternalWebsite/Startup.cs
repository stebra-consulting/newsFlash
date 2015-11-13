using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExternalWebsite.Startup))]
namespace ExternalWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
