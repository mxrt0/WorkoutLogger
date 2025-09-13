using System.ComponentModel.DataAnnotations;

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
    public Workout() { }
    public int Id { get; set; }

    [Required(ErrorMessage = "Date is required.")]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "End time is required.")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime EndTime { get; set; }

    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan Duration { get; set; }

    [Required(ErrorMessage = "Muscle group is required.")]
    public MuscleGroup MuscleGroup { get; set; }

    public void CalculateDuration() => Duration = EndTime - StartTime;
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
