using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Logios.Startup))]
namespace Logios
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
