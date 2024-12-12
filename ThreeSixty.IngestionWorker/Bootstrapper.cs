using AutoMapper;
using MediViewerLite.Common;
using MediViewerLite.Data.Context;
using MediViewerLite.IngestionWorker.Helpers;
using MediViewerLite.Services.Implementation;
using MediViewerLite.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediViewerLite.IngestionWorker
{
    public static class Bootstrapper
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .Configure<WhenToRunOptions>(
                            hostContext
                                .Configuration
                                .GetSection(WhenToRunOptions.WhenToRun));

                    services.AddHostedService<Worker>();

                    services.Configure<AppSetting>
                        (hostContext.Configuration.GetSection("AppSetting"));


                    //Database
                    var configuration = hostContext.Configuration;
                    var config = configuration["ConnectionStrings:MediViewerLite"];

                    services.AddScoped<IMediViewerLiteContext>(provider => provider.GetService<MediViewerLiteContext>() ?? throw new InvalidOperationException());

                    services.AddDbContext<MediViewerLiteContext>(
                        options => options.UseSqlServer(config), ServiceLifetime.Transient);

                    // Auto Mapper Configurations
                    var mapperConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new MappingProfile());
                    });

                    var mapper = mapperConfig.CreateMapper();
                    services.AddSingleton(mapper);

                    //Services
                    services.AddScoped<IMediViewerLiteContext, MediViewerLiteContext>();
                    services.AddScoped<ISubmissionService, SubmissionService>();
                    services.AddScoped<IIngestionService, IngestionService>();
                    services.AddScoped<IEntityService, EntityService>();
                    services.AddScoped<IDocumentService, DocumentService>();
                    services.AddScoped<IBatchSubmissionDocumentSourceConfigService, BatchSubmissionDocumentSourceConfigService>();
                    services.AddScoped<IBatchSubmissionDocumentSourceService, BatchSubmissionDocumentSourceService>();
                    services.AddScoped<IFtpService, FtpService>();
                    services.AddScoped<IFileService, FileService>();
                    services.AddScoped<IAuditEntityService, AuditEntityService>();
                    services.AddScoped<IAuditDocumentService, AuditDocumentService>();
                    services.AddScoped<IActivityAuditTrailService, ActivityAuditTrailService>();
                    services.AddScoped<IStagingSubmissionService, StagingSubmissionService>();
                    services.AddScoped<IStagingIngestionService, StagingIngestionService>();
                    services.AddScoped<IStagingEntityService, StagingEntityService>();
                    services.AddScoped<IStagingDocumentService, StagingDocumentService>();
                    services.AddScoped<IOcrService, OcrService>();


                });
    }
}
