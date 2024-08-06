using System.Net;
using MediaServerChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaServerChallenge.Controllers.Upload;

public class FilesController : Controller
{
    private readonly ILogger<FilesController> _logger;
    private readonly IUploadFilesCommand _uploadFilesCommand;
    private readonly IFetchFilesQuery _fetchFilesQuery;

    public FilesController(ILogger<FilesController> logger, IUploadFilesCommand uploadFilesCommand, IFetchFilesQuery fetchFilesQuery)
    {
        _logger = logger;
        _uploadFilesCommand = uploadFilesCommand;
        _fetchFilesQuery = fetchFilesQuery;
    }

    [HttpPost("files/")]
    [RequestSizeLimit(AppExtensions.MultipartBodyLengthLimit200Mb)] 
    public async Task<IActionResult> Upload(IFormFileCollection? files, CancellationToken cancellationToken)
    {
        if (files == null)
        {
            return StatusCode((int)HttpStatusCode.RequestEntityTooLarge);
        }

        if (files.Count == 0)
        {
            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        await _uploadFilesCommand.Handle(files, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("files/")]
    public IActionResult FetchList(CancellationToken cancellationToken)
    {
        var files = _fetchFilesQuery.Handle();
        
        return Ok(files);
    }
}