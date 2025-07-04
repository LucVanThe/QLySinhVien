using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLySinhVien.Models;

[Table("TeacherAcc")]
[Index("Email", Name = "IX_TeacherAcc", IsUnique = true)]
public partial class TeacherAcc
{
    [Key]
    public int AccId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    [Column("TeacherID")]
    public int? TeacherId { get; set; }

    [StringLength(50)]
    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public bool Active { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("TeacherAccs")]
    public virtual Teacher? Teacher { get; set; }
}
