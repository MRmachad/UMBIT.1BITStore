using BIT.WebApp.MVC.Extensions;
using BIT.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BIT.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticationService, AutenticationService>();

            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
