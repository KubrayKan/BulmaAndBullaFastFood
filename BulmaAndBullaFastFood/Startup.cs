using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BulmaAndBullaFastFood.Startup))]
namespace BulmaAndBullaFastFood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //bruh
            ConfigureAuth(app);
        }
    }
}
