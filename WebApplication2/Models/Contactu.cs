using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Contactu
{
    public decimal Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Subject { get; set; }

    public string Message { get; set; } = null!;
}
