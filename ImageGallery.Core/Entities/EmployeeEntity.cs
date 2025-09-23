using System;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class EmployeeEntity : BaseEntity
{
    public EmployeeEntity(string title, byte[] profilePhoto, string description)
    {
        Title = title;
        ProfilePhoto = profilePhoto;
        Description = description;
    }

    private EmployeeEntity()
    {
    }

    public string Title { get; private set; } = string.Empty; // todo: add this on the design UI

    public byte[] ProfilePhoto { get; private set; } = Array.Empty<byte>();

    public string Description { get; private set; } = string.Empty;

    public Guid AboutUsId { get; private set; }

    public AboutUsEntity AboutUs { get; private set; } = null!;
    // todo: remove social media icons from about.html
    // todo: add seed for about us, and make unable admin to remove the record, or add another one
}
