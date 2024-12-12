using System.Collections.ObjectModel;
using System.Data;
using System.Net;
using MediatR;
using ThreeSixty.Api.DI;
using ThreeSixty.Api.Services;
using ThreeSixty.Common.Helpers;
using ThreeSixty.Services.Interface.Common;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace ThreeSixty.Api
{
    public class Startup
    {
        private const string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //Logging
            string connectionString = Configuration.GetConnectionString("ThreeSixty");
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
               {
                   new SqlColumn("UserName", SqlDbType.NVarChar)
               }
            };
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log", SchemaName = "adt" }
                , null, null, LogEventLevel.Information, null, columnOptions: columnOptions, null, null)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(AppDomain.CurrentDomain.Load("ThreeSixty.Services.Interface"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("ThreeSixty.Services.Implementation"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("ThreeSixty.Application"));


            services.AddInfrastructure(Configuration);
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThreeSixty.API v1"));
            //}
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    // write error in http response
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthorization();
            app.Use(async (httpContext, next) =>
            {
                var userName = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "Guest"; //Gets user Name from user Identity  
                LogContext.PushProperty("Username", userName); //Push user in LogContext;  
                await next.Invoke();
            }
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
