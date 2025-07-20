namespace ImageGallery.Core.Entities;

public class Category : BaseEntity
{
    public Category(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public void AddImage(Image image)
    {
        Images.Add(image);
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    
    public ICollection<Image> Images { get; private set; }
}