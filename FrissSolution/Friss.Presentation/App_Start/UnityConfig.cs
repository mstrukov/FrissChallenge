using Friss.Application.Services;
using Friss.Domain.Entities;
using Friss.Infrastructure.DAL;
using Friss.Infrastructure.DAL.Database;
using Friss.Infrastructure.DAL.FileSystem;
using System;
using System.Web;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Friss.Presentation
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            
            container.RegisterType<IFrissDbContext, FrissDbContext>(new PerThreadLifetimeManager());
			container.RegisterType<IFrissFileSystemContext, FrissFileSystemContext>(
				new PerThreadLifetimeManager(),
				new InjectionConstructor(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)));

			container.RegisterType<IFileRepository, FrissFileRepository>();
			container.RegisterType<IEntitiesRepository<Document>, DocumentRepository>();

			container.RegisterType<IDocumentService, DocumentService>();
		}
    }
}