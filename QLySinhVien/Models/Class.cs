using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLySinhVien.Models;

[Table("Class")]
public partial class Class
{
    [Key]
    [Column("ClassID")]
    public int ClassId { get; set; }

    [StringLength(50)]
    public string? NameClass { get; set; }

    [Column("CourseID")]
    public int? CourseId { get; set; }

    [Column("TeacherID")]
    public int? TeacherId { get; set; }

    public DateOnly? ClassDate { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [ForeignKey("CourseId")]
    [InverseProperty("Classes")]
    public virtual Course? Course { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("Classes")]
    public virtual Teacher? Teacher { get; set; }
}
