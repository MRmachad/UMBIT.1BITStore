using BIT.WebApp.MVC.Configuration;

namespace BIT.WebApp.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration();
            services.AddMvcConfiguration();
            services.RegisterServices();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvcConfiguration(env);
        }
    }
}
