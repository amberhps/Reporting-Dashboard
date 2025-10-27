using ReportingDashboard.Components;
using MudBlazor.Services;
using Microsoft.Extensions.Options;
using MudBlazor;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using ReportingDashboard.Data.Warehouse;
using ReportingDashboard.Data.Jobs;
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

            builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

            builder.Services.AddAuthorization(ConfigureRoles);

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));
            builder.Services.AddScoped<WarehouseDashboardContext>();
            builder.Services.AddScoped<JobContext>();
            builder.Services.AddDbContextFactory<WarehouseConfigContext>(lifetime: ServiceLifetime.Scoped);
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton(new MudTheme()
            {
                PaletteDark = new PaletteDark()
                {
                    Primary = Colors.Blue.Darken4,
                    Secondary = Colors.Orange.Lighten1
                },

                PaletteLight = new PaletteLight()
                {
                    Primary = Colors.Blue.Darken4,
                    Secondary = Colors.Orange.Lighten1
                }
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }

        private static void ConfigureRoles(AuthorizationOptions options)
        {
            options.AddPolicy("General", policy =>
            {
                policy.RequireRole("SG-AdminPortalAdmin");
            });

            options.AddPolicy("Admin", policy =>
            {
                policy.RequireRole("RDB-Admin");
            });

            options.AddPolicy("Warehouse", policy =>
            {
                policy.RequireRole("RDB-Admin", "RDB-Warehouse");
            });

            options.AddPolicy("PharmaAPI", policy =>
            {
                policy.RequireRole("RDB-Admin", "RDB-PharmaAPI");
            });

            options.AddPolicy("Jobs", policy =>
            {
                policy.RequireRole("RDB-Admin", "RDB-Jobs");
            });

            options.FallbackPolicy = options.DefaultPolicy;
        }
    }
}
