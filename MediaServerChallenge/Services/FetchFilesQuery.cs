using FileInfo = MediaServerChallenge.Models.FileInfo;

namespace MediaServerChallenge.Services;

public class FetchFilesQueryQuery : IFetchFilesQuery
{
    private readonly string _folderPath;

    public FetchFilesQueryQuery(IFolderProvider folderProvider)
    {
        this._folderPath = folderProvider.GetFolderPath();
    }
    
    public IEnumerable<FileInfo> Handle()
    {
        var directoryInfo = new DirectoryInfo(_folderPath);

        return directoryInfo.EnumerateFiles()
            .OrderByDescending(info => info.Length)
            .Select(info => new FileInfo(info.Name, info.Length / 1024));
    }
}

public interface IFetchFilesQuery
{
    IEnumerable<FileInfo> Handle();
}