using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SozialWebApplication.Startup))]
namespace SozialWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
