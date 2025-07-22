using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class ContactEntity : BaseEntity
{
    public ContactEntity(string title, string url, byte[] logo)
    {
        Title = title;
        Url = url;
        Logo = logo;
    }

    public string Title { get; private set; }
    public string Url { get; private set; }
    public byte[] Logo { get; private set; }

    public Guid AboutUsId { get; private set; }
    public AboutUsEntity AboutUsEntity { get; private set; }
}