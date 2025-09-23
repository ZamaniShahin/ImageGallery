namespace ImageGallery.Core.Records;

public record ServiceRecord(Guid Id, string Title, string Description, decimal Price, byte[] Logo);
