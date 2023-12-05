using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public partial class About
{
    public decimal Aboutid { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? Imagepath { get; set; }
}
