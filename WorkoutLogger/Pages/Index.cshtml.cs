using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkoutLogger.Database.Entities;

namespace WorkoutLogger.Pages;

public class IndexModel : PageModel
{
    public IndexModel()
    {

    }

    public List<Workout> Workouts { get; set; }

    public void OnGet()
    {

    }
}
