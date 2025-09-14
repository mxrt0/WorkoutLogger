using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WorkoutLogger.Database;
namespace WorkoutLogger;

public class Program
{
    public static void Main(string[] args)
    {
        ExcelPackage.License.SetNonCommercialPersonal("mxrt0");
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<WorkoutLoggerDbContext>(options =>
            options.UseSqlite(
                builder.Configuration.GetConnectionString("ConnectionString"),
                 b => b.MigrationsAssembly(typeof(WorkoutLoggerDbContext).Assembly.FullName)
            )
            .UseSnakeCaseNamingConvention()
        );
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapRazorPages();

        app.Run();
    }
}
