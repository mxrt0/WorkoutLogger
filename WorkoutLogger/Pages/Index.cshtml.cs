using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkoutLogger.Database;
using WorkoutLogger.Database.Entities;

namespace WorkoutLogger.Pages;

public class IndexModel : PageModel
{
    private WorkoutLoggerDbContext _context;
    public IndexModel(WorkoutLoggerDbContext context)
    {
        _context = context;
    }

    public List<Workout> Workouts { get; set; }

    public void OnGet()
    {
        Workouts = GetAllWorkouts();
    }

    private List<Workout> GetAllWorkouts()
    {
        return _context.Workouts.ToList();
    }
}
