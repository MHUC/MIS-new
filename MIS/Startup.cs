using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MIS.Startup))]
namespace MIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
