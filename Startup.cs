using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CarWebApi.Startup))]

namespace CarWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           ConfigureAuth(app);
          
        }

        


    }
}
