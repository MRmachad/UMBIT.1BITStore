using Microsoft.AspNetCore.Authentication.Cookies;

namespace BIT.WebApp.MVC.Configuration
{
    public static class IdendentityConfig
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/login";
                    option.AccessDeniedPath = "/acesso-negado";
                    option.LogoutPath = "/logout";

                });
        }
        public static void UseIdentityConfiguration( this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
