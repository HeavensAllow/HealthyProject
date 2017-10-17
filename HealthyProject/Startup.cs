using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthyProject.Startup))]
namespace HealthyProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
