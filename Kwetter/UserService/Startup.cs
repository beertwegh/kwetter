using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.DbContext;
using UserService.Helpers.MessageBroker;
using UserService.Repository;
using UserService.Repository.Interface;
using UserService.Services;

namespace UserService
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
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, Services.UserService>();
            services.AddDbContext<UserDbContext>(o => o.UseMySQL(Configuration.GetConnectionString("UserDB")), ServiceLifetime.Singleton);
            //services.AddDbContext<UserDbContext>(o => o.UseInMemoryDatabase("UserDB"), ServiceLifetime.Singleton);
            services.AddScoped<ISendMessageBroker, SendMessageBroker>();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
