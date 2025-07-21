using RentCRL.Domain.Base;
using RentCRL.Domain.Properties;
using RentCRL.Domain.Users;
using RentCRL.Infrastructure.Base;
using RentCRL.Infrastructure.Properties;
using RentCRL.Infrastructure.Users;

namespace RentCRL.Web.DependencyInjection
{
    public static class InfrastructureServiceExtention 
    {
        public static void RegisterInfrastructureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            AddDatabaseConnection(builder, services);

            services.AddScoped<IGuidProvider, GuidProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
        }

        public static void AddDatabaseConnection(WebApplicationBuilder builder, IServiceCollection services)
        {
            var endpoint = builder.Configuration["CosmosDB:EndpointUri"];
            var primaryKey = builder.Configuration["CosmosDB:PrimaryKey"];
            var database = builder.Configuration["CosmosDB:DatabaseName"];
            services.AddSingleton(new CosmosDbService(endpoint, primaryKey, database));
        }
    }
}
