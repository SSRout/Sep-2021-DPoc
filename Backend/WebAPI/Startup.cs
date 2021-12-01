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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
                //Add Bearer to Swagger startup
                c.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme()
                {
                    Name="Authourization",
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="Bearer",
                    BearerFormat="JWT",
                    In=ParameterLocation.Header,
                    Description= "JWT Authourization header using the Bearer Scheme \r\n\r\n Enter 'Bearer'"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });
            var loggerFact = LoggerFactory.Create(conf=>conf.AddConsole());

            services.AddDbContext<VideoApplicationDbContext>(options => {
                options.UseLoggerFactory(loggerFact).UseSqlite("Data Source=VideoApplication.Db");
            });

            services.AddAuthentication(authOpt=> {
                authOpt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt=> {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"])),
                    ValidateIssuer=true,
                    ValidIssuer= Configuration["JwtConfig:Issuer"],
                    ValidateAudience=true,
                    ValidAudience= Configuration["JwtConfig:Audiance"],
                    ValidateLifetime=true
                };
            });

            //setting up DI
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

            app.UseAuthentication();

            app.UseCors("Dev-Cors");  

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
