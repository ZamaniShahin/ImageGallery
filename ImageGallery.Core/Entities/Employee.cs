namespace ImageGallery.Core.Entities;

public class Employee : BaseEntity
{
    public string Title { get; private set; } // todo: add this on the design UI
    public byte[] ProfilePhoto { get; private set; }

    public ICollection<ContactEntity> I { get; private set; }
    
}