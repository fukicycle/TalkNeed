var isScrollBottom = () => {
    const container = document.getElementById("container");
    return Math.abs(container.scrollHeight - container.clientHeight - container.scrollTop) <= 50;
};

const scrollToBottom = () => {
    const container = document.getElementById("container");
    container.scrollTop = container.scrollHeight;
}

function submit() {
    if (isScrollBottom()) {
        scrollToBottom();
    }
}

function forceScroll() {
    scrollToBottom();
}