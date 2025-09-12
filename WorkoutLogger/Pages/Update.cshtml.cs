using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkoutLogger.Database;
using WorkoutLogger.Database.Entities;

namespace WorkoutLogger.Pages;

public class UpdateModel : PageModel
{
    private WorkoutLoggerDbContext _context;
    public UpdateModel(WorkoutLoggerDbContext dbContext)
    {
        _context = dbContext;
    }

    [BindProperty]
    public Workout NewWorkout { get; set; }

    public IActionResult OnGet(int id)
    {
        NewWorkout = GetById(id);

        return Page();
    }

    private Workout GetById(int id)
    {
        return _context.Workouts.First(x => x.Id == id);
    }

    public IActionResult OnPost()
    {
        var workoutToUpdate = _context.Workouts.First(x => x.Id == NewWorkout.Id);

        workoutToUpdate.Date = NewWorkout.Date;
        workoutToUpdate.StartTime = NewWorkout.StartTime;
        workoutToUpdate.EndTime = NewWorkout.EndTime;
        workoutToUpdate.MuscleGroup = NewWorkout.MuscleGroup;

        workoutToUpdate.CalculateDuration();

        _context.SaveChanges();

        return RedirectToPage("./Success");
    }
}
