using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkoutLogger.Database;
using WorkoutLogger.Database.Entities;

namespace WorkoutLogger.Pages;

public class DeleteModel : PageModel
{
    private WorkoutLoggerDbContext _context;
    public DeleteModel(WorkoutLoggerDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet(int id)
    {
        Workout = GetById(id);

        return Page();
    }

    [BindProperty]
    public Workout Workout { get; set; }

    private Workout GetById(int id)
    {
        return _context.Workouts.First(x => x.Id == id);
    }

    public IActionResult OnPost()
    {
        _context.Workouts.Remove(Workout);
        _context.SaveChanges();
        return RedirectToPage("./Success");
    }
}
