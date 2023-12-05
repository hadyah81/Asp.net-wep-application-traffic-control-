using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public partial class Home
{
    public decimal Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? Imagepath { get; set; }
}
