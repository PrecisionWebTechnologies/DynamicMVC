using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DynamicMVC.UI.Startup))]
namespace DynamicMVC.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
