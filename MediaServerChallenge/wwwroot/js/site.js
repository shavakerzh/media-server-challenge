import {AppViewModel, AppTemplate} from "./app/index.js";
import {VideoTemplate, VideoViewModel} from "./app/video/index.js";
import {UploadTemplate, UploadViewModel} from "./app/upload/index.js";
window.addEventListener("load", async (event) => {
    ko.components.register('video-component', {
        viewModel: VideoViewModel,
        template: VideoTemplate
    });

    ko.components.register('upload-component', {
        viewModel: UploadViewModel,
        template: UploadTemplate
    });

    ko.components.register('app', {
        viewModel: AppViewModel,
        template: AppTemplate
    });

    ko.applyBindings();
});

 
