function checkNotification() {
    if (Notification.permission === 'granted') {
        console.log('通知が許可されています');
        return true;
    } else if (Notification.permission === 'denied') {
        console.log('通知が拒否されています');
    } else {
        console.log('通知の許可状態がまだ決まっていません');
    }
    return false;
}