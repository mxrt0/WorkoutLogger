using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using WorkoutLogger.Database;

namespace WorkoutLogger.Pages;

public class ExportModel : PageModel
{
    private WorkoutLoggerDbContext _context;
    public ExportModel(WorkoutLoggerDbContext dbContext)
    {
        _context = dbContext;
    }
    public IActionResult OnGet()
    {
        return Page();
    }
    public IActionResult OnGetExportExcel()
    {
        var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Workouts");

        sheet.Cells[1, 1].Value = "Date";
        sheet.Cells[1, 2].Value = "Start Time";
        sheet.Cells[1, 3].Value = "End Time";
        sheet.Cells[1, 4].Value = "Duration";
        sheet.Cells[1, 5].Value = "Muscle Group";

        using var range = sheet.Cells[1, 1, 1, 5];
        range.Style.Font.Bold = true;
        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
        SetRangeBorderAndAlignmentStyling(range);

        int row = 2;
        var workouts = _context.Workouts.OrderBy(w => w.Date).ToList();
        foreach (var workout in workouts)
        {
            sheet.Cells[row, 1].Value = workout.Date.ToString("dd-MM-yyyy");
            sheet.Cells[row, 2].Value = workout.StartTime.ToString("hh\\:mm");
            sheet.Cells[row, 3].Value = workout.EndTime.ToString("hh\\:mm");
            sheet.Cells[row, 4].Value = workout.Duration.ToString("hh\\:mm");
            sheet.Cells[row, 5].Value = workout.MuscleGroup;

            var fillColor = (row % 2 == 0) ? Color.White : Color.LightGray;
            using var rowRange = sheet.Cells[row, 1, row, 5];
            rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            rowRange.Style.Fill.BackgroundColor.SetColor(fillColor);
            rowRange.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            SetRangeBorderAndAlignmentStyling(rowRange);

            row++;
        }
        using var totalHoursRange = sheet.Cells[row, 3, row, 4];
        sheet.Cells[row, 3].Value = "Total Hours:";
        sheet.Cells[row, 4].Value = TimeSpan.FromTicks(workouts.Sum(w => w.Duration.Ticks)).ToString("hh\\:mm");
        totalHoursRange.Style.Font.Bold = true;
        totalHoursRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
        totalHoursRange.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
        SetRangeBorderAndAlignmentStyling(totalHoursRange);
        sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

        var stream = new MemoryStream();
        package.SaveAs(stream);
        stream.Position = 0;

        string fileName = "Workouts.xlsx";
        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return File(stream, contentType, fileName);
    }

    private void SetRangeBorderAndAlignmentStyling(ExcelRange range)
    {
        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
    }
}
