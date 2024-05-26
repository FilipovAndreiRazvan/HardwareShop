using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HardwareShop.Startup))]
namespace HardwareShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
