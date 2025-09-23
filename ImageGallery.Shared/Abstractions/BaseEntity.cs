using System;

namespace ImageGallery.Shared.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime? ModifiedAt { get; private set; }

    protected void Modified() => ModifiedAt = DateTime.UtcNow;
}
