import api from "./api.js";

export class AppViewModel {
    constructor() {
        this.selectedView = ko.observable('catalog');
        this.files = ko.observableArray([]);
        this.errorMessage = ko.observable('');
    }
    
    onLoad = async () =>{
        document.querySelectorAll('nav .nav-link').forEach(e => e.addEventListener('click', this.onNavbarClick))
        await this.fetchFiles();
    }

    fetchFiles = async () => {
        const files = await api.fetchFiles()
        this.files.removeAll();
        files.forEach(f => this.files.push(f));
    }

    onUploadFormSubmit = async (formData) => {
        this.onError('');
        try {
            const response = await api.uploadFiles(formData);
            
            if(response.ok) {
                await this.fetchFiles();
                this.selectedView('catalog');
            }else {
                this.onError(`An error occurred whilst uploading file(s). Response Code ${response.status}. Please try again.`)
            }

        } catch (error) {
            console.error(error);
            this.onError('Something went wrong.');
        }
    }
    
    onError(error){
        this.errorMessage(error);
    }
    
    onNavbarClick = (event) => {
        event.preventDefault();
        this.selectedView(event.target.name);
        this.errorMessage('');
    }
}

export const AppTemplate = `
<div data-bind="template: { afterRender: onLoad }">
    <div 
        class="alert alert-danger"
        data-bind="visible: errorMessage() !== '', text: errorMessage()" 
        role="alert">
    </div>
    
    <video-component 
        params="onError: onError, files: files"
        data-bind="visible: selectedView() === 'catalog'">
    </video-component>
    
    <upload-component 
        params="onError: onError, onSubmit: onUploadFormSubmit" 
        data-bind="visible: selectedView() === 'upload'">
    </upload-component>
</div>
`;