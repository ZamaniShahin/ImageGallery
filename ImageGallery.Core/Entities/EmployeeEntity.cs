using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class EmployeeEntity : BaseEntity
{
    public string Title { get; private set; } // todo: add this on the design UI
    public byte[] ProfilePhoto { get; private set; }
    public string Description { get; private set; }

    public Guid AboutUsId { get; private set; }
    public AboutUsEntity AboutUs { get; private set; }
    // todo: remove social media icons from about.html
    // todo: add seed for about us, and make unable admin to remove the record, or add another one
}