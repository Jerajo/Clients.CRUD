using Clients.Api.Validations;
using Clients.Application.Profiles;
using Clients.SqlServer;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Clients.Api
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
            var migrationsAssembly = typeof(Startup).Assembly.GetName().FullName;
            services.AddDbContext<ApplicationDBContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString(name: "DefaultConnection"),
                                sql => sql.MigrationsAssembly(migrationsAssembly))
                                .UseLazyLoadingProxies());

            services.AddControllers(
                options =>
                {
                    options.Filters.Add<ValidationResultAttribute>();
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClientDtoValidation>());

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clients.Api v1"));
            }

            app.UseRouting();

            app.UseCors(builder =>
            builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
