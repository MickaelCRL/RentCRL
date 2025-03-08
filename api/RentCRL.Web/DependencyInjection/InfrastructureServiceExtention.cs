using RentCRL.Domain.Users;
using RentCRL.Infrastructure.Users;

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
