using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public partial class Customer
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string Gender { get; set; } = null!;

    public string Ssn { get; set; } = null!;

    public string? Nationality { get; set; }

    public string Licensenumber { get; set; } = null!;

    public string LicenseCategory { get; set; } = null!;

    public string LicensingCenter { get; set; } = null!;

    public string Contactnumber { get; set; } = null!;

    public string Email { get; set; } = null!;
    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public virtual ICollection<ViloationCustomer> ViloationCustomers { get; set; } = new List<ViloationCustomer>();

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
}
