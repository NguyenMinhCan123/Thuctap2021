using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TTQK7.Startup))]
namespace TTQK7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
