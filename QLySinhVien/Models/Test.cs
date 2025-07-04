using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLySinhVien.Models;

[Table("Test")]
public partial class Test
{
    [Key]
    [Column("TestID")]
    public int TestId { get; set; }

    [Column("CourseID")]
    public int? CourseId { get; set; }

    [StringLength(100)]
    public string? TestName { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Weight { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Tests")]
    public virtual Course? Course { get; set; }

    [InverseProperty("Test")]
    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
