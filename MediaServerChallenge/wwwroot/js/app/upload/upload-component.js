export class UploadViewModel{
    constructor(params) {
        this.onError = params.onError;
        this.onSubmit = params.onSubmit;
    }
    
    async onFormSubmit (model, event) {
        event.preventDefault();
        
        const formData = new FormData(event.target);
        
        await this.onSubmit(formData);
    }
}

export const UploadTemplate = `
<form 
    class="upload-form"
    action="/files/" 
    enctype="multipart/form-data"
    data-bind="event: {submit: onFormSubmit}" 
    method="post">
    
    <input 
        type="file" 
        multiple accept="video/mp4" 
        class="form-control" 
        name="files">
        
    <button 
        class="btn btn-primary" 
        type="submit">
        Upload
    </button>
    
</form>
`;