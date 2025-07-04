using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLySinhVien.Models;

[Table("Attendance")]
public partial class Attendance
{
    [Key]
    [Column("AttendanceID")]
    public int AttendanceId { get; set; }

    [Column("ClassID")]
    public int? ClassId { get; set; }

    [Column("StudentID")]
    public int? StudentId { get; set; }

    [StringLength(500)]
    public string? Status { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("Attendances")]
    public virtual Class? Class { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Attendances")]
    public virtual Student? Student { get; set; }
}
