using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? Email { get; set; }
}
