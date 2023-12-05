using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Testimonial
{
    public decimal Testimonialid { get; set; }

    public string Content { get; set; } = null!;

    public string? Status { get; set; }
}
