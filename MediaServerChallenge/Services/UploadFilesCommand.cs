using FileInfo = MediaServerChallenge.Models.FileInfo;

namespace MediaServerChallenge.Services;

public class UploadFilesCommandCommand: IUploadFilesCommand
{
    private readonly string _folderPath;

    public UploadFilesCommandCommand(IFolderProvider folderProvider)
    {
        this._folderPath = folderProvider.GetFolderPath();
    }

    public async Task<IList<FileInfo>> Handle(IFormFileCollection fileCollection, CancellationToken cancellationToken)
    {
        var result = new List<FileInfo>();
        
        foreach (var file in fileCollection)
        {
            await CreateFile(file, cancellationToken);
            result.Add(new FileInfo(file.FileName, file.Length));
        }

        return result;
    }

    private async Task CreateFile(IFormFile file, CancellationToken cancellationToken)
    {
        var path = Path.Combine(this._folderPath, file.FileName);
        await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        await file.CopyToAsync(fileStream, cancellationToken);
    }
}

public interface IUploadFilesCommand
{
    Task<IList<FileInfo>> Handle(IFormFileCollection fileCollection, CancellationToken cancellationToken);
}