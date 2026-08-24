using RentCRL.Application.Contracts;
using RentCRL.Application.Properties;
using RentCRL.Application.Users;

namespace RentCRL.Web.DependencyInjection
{   
    public static class ApplicationServiceExtension
    {
        public static void RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IContractService, ContractService>();
        }
    }
}
