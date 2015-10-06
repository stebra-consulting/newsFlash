using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCTestFelix.Startup))]
namespace MVCTestFelix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
