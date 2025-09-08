namespace WorkoutLogger.Database.Entities;

public class Workout
{
    public Workout(DateTime date, DateTime startTime, DateTime endTime, MuscleGroup muscleGroup)
    {
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Duration = EndTime - StartTime;
        MuscleGroup = muscleGroup;
    }

    public int Id { get; set; }

    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public MuscleGroup MuscleGroup { get; set; }
}

public enum MuscleGroup
{
    UpperBody = 1,
    LowerBody,
    FullBody,
    Chest,
    Back,
    Arms,
    Shoulders,
    Legs,
    Core
}
