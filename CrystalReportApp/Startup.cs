using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrystalReportApp.Startup))]
namespace CrystalReportApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
