using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS_grupp1.Startup))]
namespace LMS_grupp1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
