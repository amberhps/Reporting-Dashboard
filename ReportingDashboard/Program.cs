using ReportingDashboard.Components;
using MudBlazor.Services;
using ReportingDashboard.Data.Models;
using ReportingDashboard.Data;

namespace ReportingDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices();

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));
            builder.Services.AddScoped<WarehouseContext>();
            builder.Services.AddScoped<SSISContext>();
            builder.Services.AddDbContextFactory<CaretendContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
