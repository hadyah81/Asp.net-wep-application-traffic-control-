using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Vehicle
{
    public decimal Vehicleid { get; set; }

    public string StructureNo { get; set; } = null!;

    public int VehicleRegistrationNumber { get; set; }

    public string RegistrationStatus { get; set; } = null!;

    public string VehicleCategory { get; set; } = null!;

    public string VehicleType { get; set; } = null!;

    public string VehicleNationality { get; set; } = null!;

    public string? Model { get; set; }

    public string Color { get; set; } = null!;

    public decimal? Year { get; set; }

    public decimal? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
}
