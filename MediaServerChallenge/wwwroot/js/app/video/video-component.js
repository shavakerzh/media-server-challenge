export class VideoViewModel {
    constructor(params){
        this.files = params.files;
        this.videoSource = ko.observable('');
    }

    onVideoClick =(row, event) => {
        this.videoSource(`${window.location.origin}/video/${row.name}`);
    }
}

export const VideoTemplate = `
<div class="video-container">
    <div>
        <video controls autoplay data-bind="attr: { src: videoSource }">
            Sorry, your browser doesn't support embedded videos
        </video>
    </div>
    <div>
        <div data-bind='visible: files().length == 0' class="alert alert-primary" role="alert">
            The Catalog is empty. Use the upload form to add videos.
        </div>
        <table class="table table-dark table-hover" data-bind='visible: files().length > 0'>
            <thead>
                <tr>
                    <th scope="col">Video Filename</th>
                    <th scope="col">File Size KB</th>
                </tr>
            </thead>
            <tbody data-bind="foreach: files">
                <tr data-bind="click: $parent.onVideoClick">
                    <td data-bind="text: name"></td>
                    <td data-bind="text: size"></td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
`;