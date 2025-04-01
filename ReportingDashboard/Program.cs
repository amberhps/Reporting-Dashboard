using ReportingDashboard.Components;
using MudBlazor.Services;
using ReportingDashboard.Data.Models;
using ReportingDashboard.Data;
using Microsoft.Extensions.Options;
using MudBlazor;
using Microsoft.AspNetCore.Authentication.Negotiate;

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

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AmberPolicy", policy =>
                {
                    policy.RequireRole("SG-AdminPortalAdmin");
                });
                options.FallbackPolicy = options.DefaultPolicy;
            });

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));
            builder.Services.AddScoped<WarehouseContext>();
            builder.Services.AddScoped<SSISContext>();
            builder.Services.AddDbContextFactory<CaretendContext>(lifetime: ServiceLifetime.Scoped);
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
    }
}
