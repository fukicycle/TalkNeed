function unloadInitialize(dotnetObjectReference) {
    window.addEventListener('beforeunload', (event) => {
        event.preventDefault();
        event.returnValue = "";
    });
    window.addEventListener('unload', (event) => {
        dotnetObjectReference.invokeMethodAsync("Close");
    });
}

