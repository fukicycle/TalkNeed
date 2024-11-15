importScripts('https://www.gstatic.com/firebasejs/11.0.2/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/11.0.2/firebase-messaging-compat.js');

// Initialize Firebase
firebase.initializeApp({
    apiKey: "AIzaSyDSB0ARDSxLjtVot3a0T778Klb2seSKo_k",
    authDomain: "test-chat-36e47.firebaseapp.com",
    databaseURL: "https://test-chat-36e47-default-rtdb.firebaseio.com",
    projectId: "test-chat-36e47",
    storageBucket: "test-chat-36e47.firebasestorage.app",
    messagingSenderId: "42540604174",
    appId: "1:42540604174:web:27a3998d86931e877e555f",
    measurementId: "G-M331Y7PSGQ"
});

const messaging = firebase.messaging();

messaging.onBackgroundMessage((payload) => {
    console.log('Received background message ', payload);
    const notificationTitle = payload.notification.title;
    const notificationOptions = {
        body: payload.notification.body,
        icon: 'icon-512.png'
    };

    self.registration.showNotification(notificationTitle, notificationOptions);
});
