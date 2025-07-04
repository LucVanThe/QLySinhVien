using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLySinhVien.Models;

public partial class QLSinhVien : DbContext
{
    public QLSinhVien()
    {
    }

    public QLSinhVien(DbContextOptions<QLSinhVien> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAcc> StudentAccs { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherAcc> TeacherAccs { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263C6AF020A7");

            entity.HasOne(d => d.Class).WithMany(p => p.Attendances).HasConstraintName("FK__Attendanc__Class__44FF419A");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances).HasConstraintName("FK__Attendanc__Stude__45F365D3");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927A098F9DE90");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes).HasConstraintName("FK__Class__CourseID__412EB0B6");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes).HasConstraintName("FK__Class__TeacherID__4222D4EF");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D718785316637");

            entity.Property(e => e.CourseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FB3FD365B9");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments).HasConstraintName("FK__Enrollmen__Cours__3E52440B");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments).HasConstraintName("FK__Enrollmen__Stude__3D5E1FD2");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.ScoreId).HasName("PK__Score__7DD229F161A73B58");

            entity.HasOne(d => d.Student).WithMany(p => p.Scores).HasConstraintName("FK__Score__StudentID__4CA06362");

            entity.HasOne(d => d.Test).WithMany(p => p.Scores).HasConstraintName("FK__Score__TestID__4BAC3F29");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A7921EC39EF");
        });

        modelBuilder.Entity<StudentAcc>(entity =>
        {
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Student).WithMany(p => p.StudentAccs).HasConstraintName("FK_StudentAcc_Student");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teacher__EDF2594441DED8AB");
        });

        modelBuilder.Entity<TeacherAcc>(entity =>
        {
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherAccs).HasConstraintName("FK_TeacherAcc_Teacher");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__8CC3310084539817");

            entity.HasOne(d => d.Course).WithMany(p => p.Tests).HasConstraintName("FK__Test__CourseID__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
