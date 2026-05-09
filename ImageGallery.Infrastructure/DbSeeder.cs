using ImageGallery.Core.Entities;
using ImageGallery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImageGallery.Infrastructure;

public static class DbSeeder
{
    // 1×1 transparent PNG
    private static readonly byte[] PlaceholderImage = Convert.FromBase64String(
        "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==");

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await SeedCategoriesAsync(db);
        await SeedServicesAsync(db);
        await SeedAboutUsAsync(db);
    }

    private static async Task SeedCategoriesAsync(AppDbContext db)
    {
        if (await db.Set<CategoryEntity>().AnyAsync()) return;

        var nature = new CategoryEntity("Nature", "Beautiful landscapes and wildlife photography from around the world.");
        nature.AddImage(new ImageEntity(nature.Id, PlaceholderImage, "Sunrise over misty mountains"));
        nature.AddImage(new ImageEntity(nature.Id, PlaceholderImage, "Dense rainforest canopy"));
        nature.AddImage(new ImageEntity(nature.Id, PlaceholderImage, "Crystal clear ocean waves"));

        var architecture = new CategoryEntity("Architecture", "Stunning buildings and urban design from modern and historic cities.");
        architecture.AddImage(new ImageEntity(architecture.Id, PlaceholderImage, "Modern glass skyscrapers at dusk"));
        architecture.AddImage(new ImageEntity(architecture.Id, PlaceholderImage, "Ancient Roman colosseum"));

        var portrait = new CategoryEntity("Portrait", "Expressive human portraits capturing emotion and personality.");
        portrait.AddImage(new ImageEntity(portrait.Id, PlaceholderImage, "Studio portrait with natural light"));
        portrait.AddImage(new ImageEntity(portrait.Id, PlaceholderImage, "Street candid in black and white"));

        var abstract_ = new CategoryEntity("Abstract", "Creative and experimental photography exploring color and form.");
        abstract_.AddImage(new ImageEntity(abstract_.Id, PlaceholderImage, "Light painting in a dark room"));

        db.Set<CategoryEntity>().AddRange(nature, architecture, portrait, abstract_);
        await db.SaveChangesAsync();
    }

    private static async Task SeedServicesAsync(AppDbContext db)
    {
        if (await db.Set<ServiceEntity>().AnyAsync()) return;

        db.Set<ServiceEntity>().AddRange(
            new ServiceEntity("Portrait Session", "Professional 1-hour portrait photography session with edited digital files.", 150m, PlaceholderImage),
            new ServiceEntity("Event Coverage", "Full-day event photography including corporate events, conferences, and parties.", 600m, PlaceholderImage),
            new ServiceEntity("Product Photography", "High-quality product photos optimised for e-commerce and print marketing.", 80m, PlaceholderImage),
            new ServiceEntity("Landscape Prints", "Large-format fine-art prints of landscape photographs, framed or unframed.", 200m, PlaceholderImage)
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedAboutUsAsync(AppDbContext db)
    {
        if (await db.Set<AboutUsEntity>().AnyAsync()) return;

        var about = new AboutUsEntity(
            "About Image Gallery",
            "Capturing the world one frame at a time",
            "We are a passionate team of photographers dedicated to showcasing the beauty of the world through our lens. Founded in 2020, Image Gallery has grown into a vibrant community of artists sharing their unique perspectives.",
            PlaceholderImage
        );

        about.AddEmployee(new EmployeeEntity("Alex Morgan", PlaceholderImage, "Lead photographer with 10 years of experience in landscape and portrait photography."));
        about.AddEmployee(new EmployeeEntity("Sara Chen", PlaceholderImage, "Creative director and fine-art photographer specialising in abstract compositions."));
        about.AddEmployee(new EmployeeEntity("David Kim", PlaceholderImage, "Event photographer and photo editor, ensuring every shot tells a story."));

        about.AddContact(new ContactEntity("Email", "mailto:info@imagegallery.dev", PlaceholderImage));
        about.AddContact(new ContactEntity("Instagram", "https://instagram.com/imagegallery", PlaceholderImage));
        about.AddContact(new ContactEntity("Twitter", "https://twitter.com/imagegallery", PlaceholderImage));

        db.Set<AboutUsEntity>().Add(about);
        await db.SaveChangesAsync();
    }
}
