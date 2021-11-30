using InnoTech.VideoApplication2021.Domain.Services;
//using InnoTech.VideoApplication2021.SQL.Repositories;//was replaced with Efcore
using InnotTech.VideoApplication2021.Core.IServices;
using InnoTech.VideoApplication2021.Domain.IRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DPoc.Efcore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DPoc.Efcore.Repositories;
using AppData.Security.Services;

namespace WebAPI
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
            var value = Configuration["JwtConfig:Secret"];
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
            var loggerFact = LoggerFactory.Create(conf=>conf.AddConsole());

            services.AddDbContext<VideoApplicationDbContext>(options => {
                options.UseLoggerFactory(loggerFact).UseSqlite("Data Source=VideoApplication.Db");
            });
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IGenreServices, GenreService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ISecurityService, SecurityService>();

            services.AddCors(options =>
            {
                options.AddPolicy("Dev-Cors",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,VideoApplicationDbContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));

                ctx.Database.EnsureDeleted();//If Exists id Development mode only to avoid migration
                ctx.Database.EnsureCreated();
            }

            app.UseRouting();

             app.UseCors("Dev-Cors");  

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
