using Microsoft.EntityFrameworkCore;
using WorkoutLogger.Database.Entities;
namespace WorkoutLogger.Database;

public class WorkoutLoggerDbContext : DbContext
{
    public WorkoutLoggerDbContext(DbContextOptions options) : base(options) { }

    public WorkoutLoggerDbContext()
    {

    }

    public DbSet<Workout> Workouts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workout>()
            .Property(w => w.MuscleGroup)
            .HasConversion<string>();
        base.OnModelCreating(modelBuilder);
    }

}
