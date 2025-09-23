using System;
using System.Collections.Generic;
using ImageGallery.Shared.Abstractions;

namespace ImageGallery.Core.Entities;

public class AboutUsEntity : BaseEntity
{
    public AboutUsEntity(string title, string h2Title, string description, byte[] image)
    {
        Title = title;
        H2Title = h2Title;
        Description = description;
        Image = image;
    }

    private AboutUsEntity()
    {
    }

    public string Title { get; private set; } = string.Empty;

    public string H2Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public byte[] Image { get; private set; } = Array.Empty<byte>();

    public ICollection<EmployeeEntity> Employees { get; private set; } = new List<EmployeeEntity>();

    public ICollection<ContactEntity> ContactEntities { get; private set; } = new List<ContactEntity>();

    public void Update(string title, string h2Title, string description, byte[] image)
    {
        Title = title;
        H2Title = h2Title;
        Description = description;
        Image = image;
        Modified();
    }

    public void AddEmployee(EmployeeEntity employee)
    {
        Employees.Add(employee);
    }

    public void AddContact(ContactEntity contact)
    {
        ContactEntities.Add(contact);
    }
}
