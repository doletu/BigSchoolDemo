using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BigSchoolDemo.Startup))]
namespace BigSchoolDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
