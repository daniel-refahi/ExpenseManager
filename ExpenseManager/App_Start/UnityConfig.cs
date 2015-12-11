using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ExpenseManager.Controllers;
using ExpenseManger.Repository;

namespace ExpenseManager.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion
        
        public static void RegisterTypes(IUnityContainer container)
        {            
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IManagerRepository, ManagerRepository>();
        }
    }
}
