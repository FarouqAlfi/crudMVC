using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudMVC.Startup))]
namespace CrudMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
