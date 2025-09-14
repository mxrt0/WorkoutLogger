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
        ViewData["Total"] = TimeSpan.FromTicks(Workouts.Sum(x => x.Duration.Ticks)).ToString("hh\\:mm");
    }

    private List<Workout> GetAllWorkouts()
    {
        return _context.Workouts.OrderBy(w => w.Date).ToList();
    }
}
