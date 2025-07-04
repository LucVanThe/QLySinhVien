using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLySinhVien.Models;

[Table("Student")]
public partial class Student
{
    [Key]
    [Column("StudentID")]
    public int StudentId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [Column("DOB")]
    public DateOnly? Dob { get; set; }

    [StringLength(50)]
    public string? Gender { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [InverseProperty("Student")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [InverseProperty("Student")]
    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentAcc> StudentAccs { get; set; } = new List<StudentAcc>();
}
