using System;
using System.Web;
using System.Linq;
using AutoMapper;
using HardwareShop.MappingProfiles;
using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Ninject.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HardwareShop.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(HardwareShop.App_Start.NinjectWebCommon), "Stop")]

namespace HardwareShop.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Locul unde legăm serviciile noastre.
        /// </summary>
        /// <param name="kernel">Containerul de IoC.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Aici faci legăturile (Binding-urile)

            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            // Dacă ai și alte servicii, le legi aici!
            kernel.Bind<IMapper>().ToMethod(ctx =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MyProfile>();
                });
                return config.CreateMapper();
            });
        }
    }
}
