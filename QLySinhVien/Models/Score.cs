using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLySinhVien.Models;

[Table("Score")]
public partial class Score
{
    [Key]
    [Column("ScoreID")]
    public int ScoreId { get; set; }

    [Column("TestID")]
    public int? TestId { get; set; }

    [Column("StudentID")]
    public int? StudentId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Marks { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Scores")]
    public virtual Student? Student { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("Scores")]
    public virtual Test? Test { get; set; }
}
