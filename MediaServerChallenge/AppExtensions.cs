using MediaServerChallenge.Controllers;
using MediaServerChallenge.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

namespace MediaServerChallenge;

public static class AppExtensions
{
    public const long MultipartBodyLengthLimit200Mb = 200 * 1024 * 1024;

    public static IServiceCollection AddMediaServer(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = MultipartBodyLengthLimit200Mb; })
            .AddSingleton<IFetchFilesQuery, FetchFilesQueryQuery>()
            .AddSingleton<IUploadFilesCommand, UploadFilesCommandCommand>()
            .AddSingleton<IFolderProvider, FolderProvider>()
            .AddControllersWithViews(options => options.Filters.Add<GlobalExceptionFilter>());

        return serviceCollection;
    }

    public static IApplicationBuilder UseVideoFiles(this IApplicationBuilder app)
    {
        var folderProvider = app.ApplicationServices.GetRequiredService<IFolderProvider>();
        var folderPath = folderProvider.GetFolderPath();
        folderProvider.EnsureFolderPathExists();
        
        return app.UseStaticFiles(new StaticFileOptions
        {
            RequestPath = "/video",
            HttpsCompression = HttpsCompressionMode.Compress,
            FileProvider = new PhysicalFileProvider(folderPath)
        });
    }
}