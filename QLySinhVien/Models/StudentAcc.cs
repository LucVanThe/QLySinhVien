using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLySinhVien.Models;

[Table("StudentAcc")]
[Index("Email", Name = "IX_StudentAcc", IsUnique = true)]
public partial class StudentAcc
{
    [Key]
    public int AccId { get; set; }

    [StringLength(500)]
    public string? FirstName { get; set; }

    [StringLength(500)]
    public string? LastName { get; set; }

    [Column("DOB")]
    public DateOnly? Dob { get; set; }

    [StringLength(50)]
    public string? Gender { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    [Column("StudentID")]
    public int? StudentId { get; set; }

    [StringLength(50)]
    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public bool Active { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentAccs")]
    public virtual Student? Student { get; set; }
}
