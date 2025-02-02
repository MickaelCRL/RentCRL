using RentCRL.Application;

namespace RentCRL.Web.DependencyInjection
{
    public static class ApplicationServiceExtension
    {
        public static void RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddTransient<IOwnerService, OwnerService>();
        }
    }
}
