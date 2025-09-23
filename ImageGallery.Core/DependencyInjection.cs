using AboutUsServices = ImageGallery.Core.Services.GetAboutUs;
using CategoryServices = ImageGallery.Core.Services.Category;
using ServiceServices = ImageGallery.Core.Services.Service;

namespace Microsoft.Extensions.DependencyInjection;

public static class CoreServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryServices.AddHandler>();
        services.AddScoped<CategoryServices.GetAllHandler>();
        services.AddScoped<CategoryServices.GetAllImagesHandler>();
        services.AddScoped<ServiceServices.AddHandler>();
        services.AddScoped<ServiceServices.DeleteHandler>();
        services.AddScoped<ServiceServices.GetAllHandler>();
        services.AddScoped<ServiceServices.UpdateHandler>();
        services.AddScoped<AboutUsServices.GetHandler>();
        services.AddScoped<AboutUsServices.UpdateHandler>();

        return services;
    }
}
