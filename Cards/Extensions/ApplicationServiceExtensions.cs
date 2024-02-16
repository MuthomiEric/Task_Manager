using Cards.App_Services;
using Cards.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Cards.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddDbContext<CardDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Card")));

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<ICardRepository, CardRepository>();

            services.AddSingleton<IDateTimeFactory, DefaultDateTimeFactory>();

            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfiles>());

            services.AddCors();

            return services;
        }

        public static IApplicationBuilder UseApplicationServices(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            return app;
        }
    }
}