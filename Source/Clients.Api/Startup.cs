using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Serilog;

using Clients.Api.Validations;
using Clients.Application.Profiles;
using Clients.SqlServer;

namespace Clients.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
#if MOCK
            services.AddDbContext<ApplicationDBContext>(options =>
                            options.UseSqlite("Data Source=sqlite.db"));
            // If you want to do a quick test.
            // services.AddDbContext<ApplicationDBContext>(options =>
            //                 options.UseInMemoryDatabase("applicationDb"));
#else
            var migrationsAssembly = typeof(Startup).Assembly.GetName().FullName;
            services.AddDbContext<ApplicationDBContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString(name: "DefaultConnection"),
                                sql => sql.MigrationsAssembly(migrationsAssembly))
                                .UseLazyLoadingProxies());
#endif

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        // builder.WithOrigins("http://10.0.0.2:8081",
                        //                     "http://localhost::8081");
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddControllers(
                options =>
                {
                    options.Filters.Add<ValidationResultAttribute>();
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddressDtoValidation>());

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(ClientsProfile).Assembly);

            services.AddTransient<IValidatorInterceptor, DtoValidatorInterceptor>();

            services.ConfigIoCServices();
            services.ConfigIoCForFactories();
            services.ConfigIoCForCommands();
            services.ConfigIoCForQueries();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clients.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "*/*";

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is Exception ex)
                    {
                        await Task.Run(() =>
                        {
                            Log.Fatal("Server-side Error: {0}", ex.Message);
                        });
                    }
                });
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clients.Api v1");
                        c.RoutePrefix = string.Empty;
                    });
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
