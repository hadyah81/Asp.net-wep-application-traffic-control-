using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Role
{
    public decimal Id { get; set; }

    public string? Rolename { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
