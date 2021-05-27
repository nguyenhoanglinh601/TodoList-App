using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo_List.Models.DB_Settings;
using Todo_List.Services;
using Microsoft.Net.Http.Headers;

namespace Todo_List
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
            //FOR LOCAL DEVELOPING
            //services.AddCors(
            //   options => options.AddPolicy("MyAllowHeadersPolicy",
            //   builder =>
            //   {
            //       builder.WithOrigins("http://localhost:4200")
            //              .WithHeaders(HeaderNames.ContentType, "x-custom-header");
            //   })
            //);

            //FOR DELOYING
            services.AddCors(
               options => options.AddPolicy("MyAllowHeadersPolicy",
               builder =>
               {
                   builder.WithOrigins("https://todolist-hoanglinh.web.app")
                          .WithHeaders(HeaderNames.ContentType, "x-custom-header");
               })
            );

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddSingleton<ItemService>();

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo_List", Version = "v1" });
            //});

            services.Configure<TodoListDatabaseSettings>(Configuration.GetSection(nameof(TodoListDatabaseSettings)));

            services.AddSingleton<MyTodoListDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TodoListDatabaseSettings>>().Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo_List v1"));
            }

            //FOR LOCAL DEVELOPING
            //app.UseCors(
            //   options => options.WithOrigins("http://localhost:4200")
            //   .AllowAnyMethod()
            //   .AllowAnyHeader()
            //   .AllowCredentials()
            //);

            //FOR DEPLOYING
            app.UseCors(
               options => options.WithOrigins("https://todolist-hoanglinh.web.app")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
