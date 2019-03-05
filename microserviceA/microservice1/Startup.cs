using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System.Reflection;
using System.Web.Http;

namespace microservice1
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            appBuilder.UseNinjectMiddleware(CreateKernel);
            appBuilder.UseNinjectWebApi(config);
        }

        IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();

            kernel.Load(Assembly.GetExecutingAssembly());

            return kernel;
        }
    }
}