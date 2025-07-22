using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class AboutUsEntity : BaseEntity
{
    public string Title { get; private set; }
    public string H2Title { get; private set; }
    public string Description { get; private set; }
    public byte[] Image { get; private set; }

    public ICollection<EmployeeEntity> Employees { get; private set; }
    public ICollection<ContactEntity> ContactEntities { get; private set; }

    public AboutUsEntity(string title, string h2Title, string description, byte[] image)
    {
        Title = title;
        H2Title = h2Title;
        Description = description;
        Image = image;
    }

    public void Update(string title, string h2Title, string description, byte[] image)
    {
        Title = title;
        H2Title = h2Title;
        Description = description;
        Image = image;
        Modified();// todo: add this on all update methods
    }

    public void AddEmployee(EmployeeEntity employee)
    {
        Employees.Add(employee);
    }
}