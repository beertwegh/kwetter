using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageService.Helpers.MessageBroker;
using MessageService.Helpers.MessageBroker.Connection;
using MessageService.Messaging;
using MessageService.DbContext;
using MessageService.Helpers.MessageBroker;
using MessageService.Repository;
using MessageService.Repository.Interface;
using MessageService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MessageService
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
            services.AddDbContext<MessageDbContext>(o => o.UseMySQL(Configuration.GetConnectionString("MessageDB")), ServiceLifetime.Singleton);

            //services.AddDbContext<MessageContext>(conf => conf.UseInMemoryDatabase("MessageDB"), ServiceLifetime.Singleton);
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, Services.MessageService>();

            services.AddTransient<IReceiver, Receiver>();
            services.AddTransient<ISendMessageBroker, SendMessageBroker>();
            services.AddSingleton<IPersistentConnection, PersistentConnection>();
            services.AddTransient<IRpcClient, RpcClient>();
            services.AddTransient<IRpcCalls, RpcCalls>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
