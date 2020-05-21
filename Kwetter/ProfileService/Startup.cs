using AuthService.Helpers.MessageBroker;
using AuthService.Helpers.MessageBroker.Connection;
using AuthService.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfileService.DbContext;
using ProfileService.Repository;
using ProfileService.Services;
using System.Text;

namespace ProfileService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // configure strongly typed settings objects
            var secret = Configuration.GetValue<string>("AppSettings:Secret");
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddDbContext<ProfileContext>(o => o.UseMySQL(Configuration.GetConnectionString("ProfileDB")), ServiceLifetime.Singleton);

            //services.AddDbContext<ProfileContext>(conf => conf.UseInMemoryDatabase("ProfileDB"), ServiceLifetime.Singleton);
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProfileService, Services.ProfileService>();

            services.AddTransient<IReceiver, Receiver>();
            services.AddSingleton<IPersistentConnection, PersistentConnection>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IReceiver receiver)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
