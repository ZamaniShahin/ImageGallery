namespace ImageGallery.Shared.Abstractions;

public abstract class BaseEntity
{
    public Guid Id
    {
        get => Id;
        set => value = Guid.NewGuid();
    }


    public DateTime CreatedAt
    {
        get => CreatedAt;
        set => value = DateTime.Now;
    }
    protected void Modified() => this.ModifiedAt = DateTime.Now;

    public DateTime? ModifiedAt { get; private set; } = null;
}