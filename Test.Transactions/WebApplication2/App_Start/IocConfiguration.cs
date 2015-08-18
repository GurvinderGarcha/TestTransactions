using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Test.Transactions.Core;
using Test.Transactions.Infrastructure;
using WebApplication2.Infrastructure;

namespace WebApplication2
{
    public static class IocConfiguration
    {
        public static void ConfigureIoc()
        {
            IUnityContainer container = new UnityContainer();
            RegisterAllServices(container);
            MvcUnityContainer.Container = container;
            DependencyResolver.SetResolver(new TypeDependencyResolver(container));
            
        }

        private static void RegisterAllServices(IUnityContainer container)
        {
            container.RegisterType<IConnectionStringProvider, ConnectionStringProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDbConnectionProvider, DbConnectionProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICurrencyService, CurrencyService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITransactionsService, TransactionsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IParserService, CsvParserService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITransactionDataService, TransactionDataService>(new ContainerControlledLifetimeManager());
            
        }
    }
}