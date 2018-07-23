using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tahri_Company.Startup))]
namespace Tahri_Company
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
