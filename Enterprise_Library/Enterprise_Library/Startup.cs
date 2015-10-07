using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Enterprise_Library.Startup))]
namespace Enterprise_Library
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
