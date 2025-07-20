namespace ImageGallery.Core.Entities;

public class ContactEntity : BaseEntity
{
    public ContactEntity(string title, string url, string logo)
    {
        Title = title;
        Url = url;
        Logo = logo;
    }

    public string Title { get; private set; }
    public string Url { get; private set; }
    public string Logo { get; private set; }
}