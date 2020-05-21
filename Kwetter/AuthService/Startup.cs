using AuthService.DbContext;
using AuthService.Repository;
using AuthService.Repository.Interface;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthService.Helpers;
using AuthService.Helpers.MessageBroker;
using AuthService.Helpers.MessageBroker.Connection;
using AuthService.Messaging;
using AuthService.Models;

namespace AuthService
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
            services.AddDbContext<AuthContext>(o => o.UseMySQL(Configuration.GetConnectionString("AuthDB")), ServiceLifetime.Singleton);
            //services.AddDbContext<AuthContext>(o => o.UseInMemoryDatabase("AuthDB"), ServiceLifetime.Singleton);
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();


            services.AddCors();


            // configure strongly typed settings objects
            var secret = Configuration.GetValue<string>("AppSettings:Secret");
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Kwetter", x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            // configure DI for application services
            services.AddTransient<IAuthService, Services.AuthService>();
            services.AddTransient<IRpcServer, RpcServer>();
            services.AddTransient<IReceiver, Receiver>();
            services.AddSingleton<IPersistentConnection, PersistentConnection>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRpcServer server, IReceiver receiver)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
