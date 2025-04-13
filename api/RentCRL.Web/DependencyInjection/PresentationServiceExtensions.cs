using FluentValidation;
using RentCRL.Presentation;

namespace RentCRL.Web.DependencyInjection
{
    public static class PresentationServiceExtensions
    {
        public static void RegisterPresentationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblyContaining<AssemblyReference>();
        }
    }
}
