using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotNetBucketMvc.Startup))]
namespace DotNetBucketMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
