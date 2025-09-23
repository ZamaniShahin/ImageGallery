namespace ImageGallery.Core.Records;

public record AboutUsRecord(string Title, string H2Title, string Description, byte[] Image, List<EmployeeRecord> Employees);
