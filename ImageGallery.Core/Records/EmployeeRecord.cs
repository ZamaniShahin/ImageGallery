namespace ImageGallery.Core.Records;

public record EmployeeRecord(Guid Id, string Title, string Description, byte[] ProfilePhoto);