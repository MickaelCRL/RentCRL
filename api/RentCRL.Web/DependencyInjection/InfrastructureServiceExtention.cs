using Microsoft.Azure.Cosmos;
using RentCRL.Domain.Base;
using RentCRL.Domain.Properties;
using RentCRL.Domain.Users;
using RentCRL.Infrastructure.Base;
using RentCRL.Infrastructure.Database;
using RentCRL.Infrastructure.Properties;
using RentCRL.Infrastructure.Users;

namespace RentCRL.Web.DependencyInjection
{
    public static class InfrastructureServiceExtention 
    {
        public static void RegisterInfrastructureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            AddDatabaseConnection(builder);

            services.AddTransient<CosmosDbCreator>();
            services.AddScoped<IGuidProvider, GuidProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
        }

        public static void AddDatabaseConnection(WebApplicationBuilder builder)
        {
            var cosmosDbSettingsSection = builder.Configuration.GetSection(nameof(CosmosDbSettings));
            var cosmosDbSettings = cosmosDbSettingsSection.Get<CosmosDbSettings>();

            builder.Services.AddSingleton(cosmosDbSettings);

            builder.Services.AddSingleton(new CosmosClient(cosmosDbSettings.EndpointUri, cosmosDbSettings.PrimaryKey));
        }
    }
}
