using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class ServiceEntity : BaseEntity
{
    public ServiceEntity(string title, string description, decimal price, byte[] logo)
    {
        Title = title;
        Description = description;
        Price = price;
        Logo = logo;
    }

    public void Update(string title, string description, decimal price, byte[] logo)
    {
        Title = title;
        Description = description;
        Price = price;
        Logo = logo;
    }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public byte[] Logo { get; private set; }
}