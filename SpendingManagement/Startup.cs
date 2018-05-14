using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpendingManagement.Startup))]
namespace SpendingManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
