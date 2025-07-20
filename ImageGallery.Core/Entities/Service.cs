namespace ImageGallery.Core.Entities;

public class Service : BaseEntity
{
    public Service(string title, string description, decimal price, byte[] logo)
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