using RentCRL.Domain;
using RentCRL.Infrastructure;

namespace RentCRL.Web.DependencyInjection
{
    public static class InfrastructureServiceExtention 
    {
        public static void RegisterInfrastructureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddTransient<IOwnerRepository, OwnerRepository>();

        }

    }
}
