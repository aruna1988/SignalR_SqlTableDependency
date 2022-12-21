using SignalR_SqlTableDependency.SubscribeTableDependencies;

namespace SignalR_SqlTableDependency.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        //public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
        //    where T : ISubscribeTableDependency
        public static void UseProductTableDependency(this IApplicationBuilder applicationBuilder)
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            //var service = serviceProvider.GetService<T>();
            var service = serviceProvider.GetService<SubscribeProductTableDependency>();
            //service.SubscribeTableDependency(connectionString);
            service.SubscribeTableDependency();
        }
    }
}
