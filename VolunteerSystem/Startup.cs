using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VolunteerSystem.Startup))]
namespace VolunteerSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
