using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Violation
{
    public decimal Violationid { get; set; }

    public decimal? CustomerId { get; set; }

    public decimal? Vehicleid { get; set; }

    public string PoliceDirectorate { get; set; } = null!;

    public string CourtName { get; set; } = null!;

    public DateTime ViolationDate { get; set; }

    public DateTime ViolationTime { get; set; }

    public string ViolationLocation { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string ViolationDescription { get; set; } = null!;

    public decimal? Fineamount { get; set; }

    public string? Citationnumber { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Vehicle? Vehicle { get; set; }

    public virtual ICollection<ViloationCustomer> ViloationCustomers { get; set; } = new List<ViloationCustomer>();
}
