namespace MediaServerChallenge.Services;

public class FolderProvider: IFolderProvider
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _configuration;

    public FolderProvider(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        _webHostEnvironment = webHostEnvironment;
        _configuration = configuration;
    }

    public string GetFolderPath()
    {
        var path = _configuration.GetValue<string?>("MediaServerSettings:FolderPath");
        if (string.IsNullOrEmpty(path))
        {
            path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        }

        return path;
    }

    public void EnsureFolderPathExists(bool createIfNeeded = true)
    {
        var folderPath = this.GetFolderPath();
        if (Directory.Exists(folderPath))
        {
            return;
        }
        
        if (!createIfNeeded)
        {
            throw new Exception($"{folderPath} not found");
        }
        
        Directory.CreateDirectory(folderPath);
    }
}

public interface IFolderProvider
{
    string GetFolderPath();
    void EnsureFolderPathExists(bool createIfNeeded = true);
}