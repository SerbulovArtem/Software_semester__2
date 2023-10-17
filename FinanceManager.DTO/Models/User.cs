using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.DTO;

[Table("User")]
public partial class User
{
    [Key]
    [StringLength(32)]
    public string Login { get; set; } = null!;

    [StringLength(32)]
    public string Password { get; set; } = null!;
}
