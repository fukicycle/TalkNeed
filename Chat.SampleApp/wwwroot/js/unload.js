function unloadInitialize(dotnetObjectReference) {
    window.addEventListener('unload', (event) => {
        dotnetObjectReference.invokeMethodAsync("Close");
    });
}

