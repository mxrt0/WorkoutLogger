using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkoutLogger.Database;
using WorkoutLogger.Database.Entities;

namespace WorkoutLogger.Pages;

public class CreateModel : PageModel
{
    private readonly WorkoutLoggerDbContext _context;
    public CreateModel(WorkoutLoggerDbContext context)
    {
        _context = context;
    }
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Workout Workout { get; set; }

    public bool ShowInvalidTimeHeader { get; set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Workout.EndTime < Workout.StartTime)
        {
            ShowInvalidTimeHeader = true;
            return Page();
        }

        _context.Workouts.Add(new Workout(Workout.Date, Workout.StartTime, Workout.EndTime, Workout.MuscleGroup));
        _context.SaveChanges();
        return RedirectToPage("./Success");
    }
}
