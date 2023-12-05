using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class ViloationCustomer
{
    public decimal Id { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public decimal? CustomerId { get; set; }

    public decimal? Violationid { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Violation? Violation { get; set; }
}
