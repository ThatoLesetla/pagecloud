using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PageCloud.Startup))]
namespace PageCloud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
