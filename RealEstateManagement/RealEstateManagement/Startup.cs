using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealEstateManagement.Startup))]
namespace RealEstateManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
