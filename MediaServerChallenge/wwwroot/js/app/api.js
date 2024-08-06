class Api {
    async fetchFiles() {
        const response = await fetch('/files/');
        return await response.json();
    }

    async uploadFiles(formData) {
        return  await fetch('/files/', {
            method: 'POST',
            body: formData
        });
    }
}

export default new Api();