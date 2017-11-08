using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ActionFilterExample.Startup))]
namespace ActionFilterExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
