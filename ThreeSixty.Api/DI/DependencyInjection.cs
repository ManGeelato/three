using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation;
using MediatR;
using ThreeSixty.Api.Helpers;
using ThreeSixty.Data.Context;
using ThreeSixty.Services.Implementation;
using ThreeSixty.Services.Implementation.Common;
using ThreeSixty.Services.Implementation.Common.Behaviours;
using ThreeSixty.Services.Implementation.Common.Identity;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ThreeSixty.Api.DI
{
    public static class DependencyInjection
    {
        static readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)//, IWebHostEnvironment environment)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ThreeSixty Lite API", Version = "v1" });
                c.CustomSchemaIds(type => type.ToString());

                 // To Enable authorization using Swagger (JWT)    
                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             }
                         },
                         Array.Empty<string>()

                     }
                });
            });


            //Authentication
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options =>
            //     {
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            //                 .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //     });
            //


            var config = configuration["ConnectionStrings:ThreeSixty"];
            //
            // //Database
            services.AddScoped<IThreeSixtyContext>(provider => provider.GetService<ThreeSixtyContext>() ?? throw new InvalidOperationException());

            services.AddDbContext<ThreeSixtyContext>(
                 options => options.UseSqlServer(config, sqlServerOptionsAction: sqlOptions => {
                     sqlOptions.EnableRetryOnFailure();
                 }));
            //

            // services.AddHealthChecks()
            //     .AddDbContextCheck<HealthChecksDbContext>();
            //
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ThreeSixtyContext>();


            //Services
            services.AddScoped<IThreeSixtyContext, ThreeSixtyContext>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IEntityService, EntityService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<ISuburbService, SuburbService>();
            services.AddScoped<IIncidentStatusService, IncidentStatusService>();
            services.AddScoped<IDashboardService, DashboardService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddAuthorization();

            services.AddControllers();
            

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                })
                .AddIdentityServerJwt();

            return services;
        }
    }
}
