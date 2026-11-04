using Microsoft.EntityFrameworkCore;
using TH_Lab6.Models;

namespace TH_Lab6.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    public DbSet<Major> Majors => Set<Major>();
    public DbSet<Learner> Learners => Set<Learner>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Major>().ToTable("Major");
        modelBuilder.Entity<Learner>().ToTable("Learner");
        modelBuilder.Entity<Course>().ToTable("Course");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment");

        modelBuilder.Entity<Learner>()
            .HasOne(l => l.Major)
            .WithMany(m => m.Learners)
            .HasForeignKey(l => l.MajorID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Learner)
            .WithMany(l => l.Enrollments)
            .HasForeignKey(e => e.LearnerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
