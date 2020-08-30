﻿const applicationServerPublicKey = 'BCeIp5WJkDLy_2HizdSKX7euoItMfz8erK3jG362igiLstqq87qzIRcjxWxeV4tEYLMbCEJsh1L0LanEE3oOVz4';
const serviceWorker = 'sw.js';

const docReady = fn => {
    if (document.readyState === "complete" || document.readyState === "interactive") {
        setTimeout(fn, 1);
    } else {
        document.addEventListener("DOMContentLoaded", fn);
    }
};

docReady(async () => {
    const permission = await Notification.requestPermission();
    if (permission === 'granted')
        await navigator.serviceWorker.register(serviceWorker);

    const reg = await navigator.serviceWorker.ready;

    console.log(reg);
    console.log(await reg.pushManager.getSubscription());

    const subscribeParams = {
        applicationServerKey: await urlB64ToUint8Array(applicationServerPublicKey),
        userVisibleOnly: true
    };

    const subscription = await reg.pushManager.subscribe(subscribeParams);

    const p256dh = await base64Encode(subscription.getKey('p256dh'));
    const auth = await base64Encode(subscription.getKey('auth'));
    const endpoint = subscription.endpoint;

    console.log("p256dh: ", p256dh);
    console.log("auth: ", auth);
    console.log("endpoint: ", endpoint);
    console.log("subscription:", subscription);
});

const urlB64ToUint8Array = async base64String => {
    var padding = '='.repeat((4 - base64String.length % 4) % 4);
    var base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');

    var rawData = window.atob(base64);
    var outputArray = new Uint8Array(rawData.length);

    for (var i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}

let base64Encode = async arrayBuffer =>
    btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
