using RefactorMe.Application.Services;
using RefactorMe.Domain.Services;
using RefactorMe.Infra.Data.Repository;
using RTI.SimpleInjector.Extensions;
using SimpleInjector;
using System.Reflection;
using RefactorMe.Domain.Interfaces.Repository;
using RefactorMe.Infra.Data.Context;
using RefactorMe.Infra.Data.UnitOfWork;

namespace RefactorMe.CrossCutting.IoC
{
    public static class ServiceRegistration
    {
        public static void Register(Container container)
        {
            container.RegisterByConvention(Assembly.GetAssembly(typeof(ProductService)), Lifestyle.Scoped,
                "RefactorMe.Domain.Services");

            container.RegisterByConvention(Assembly.GetAssembly(typeof(ProductAppService)), Lifestyle.Scoped,
                "RefactorMe.Application.Services");

            container.RegisterByConvention(Assembly.GetAssembly(typeof(ProductRepository)), Lifestyle.Scoped,
                "RefactorMe.Infra.Data.Repository");

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            container.Register(() => new RefactorMeDataContext("name=RefactorMeDataContext"), Lifestyle.Scoped);
        }
    }
}
