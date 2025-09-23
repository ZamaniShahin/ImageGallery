using System;
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

    private ContactEntity()
    {
    }

    public string Title { get; private set; } = string.Empty;

    public string Url { get; private set; } = string.Empty;

    public byte[] Logo { get; private set; } = Array.Empty<byte>();

    public Guid AboutUsId { get; private set; }

    public AboutUsEntity AboutUsEntity { get; private set; } = null!;
}
