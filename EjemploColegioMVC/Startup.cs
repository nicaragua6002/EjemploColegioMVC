using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EjemploColegioMVC.Startup))]
namespace EjemploColegioMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
