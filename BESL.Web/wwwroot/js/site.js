﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
var connection = new signalR.HubConnectionBuilder().withUrl("/userNotificationHub").build();

function alertFadeFunc() {
    window.setTimeout(function () {
        $("#alertDiv").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
            element.style.display = "hidden";
        });
    }, 7000);

}
connection.on("ReceivePushNotification", function (messageHeader, message, type) {
    let element = document.getElementById("alertPanel");
    element.style.display = "block";
    let alertType = '';
    switch (type) {
        case 'Error':
            alertType = 'alert-danger';
        case 'Warning':
            alertType = 'alert-warning';
        case 'Info':
            alertType = 'alert-primary';
        default:
            alertType = 'alert-success';
    }
    let html = `<div id="alertDiv" class="alert ${alertType} alert-dismissible fade show" role="alert"><strong>${messageHeader}</strong> ${message}<button type = "button" class="close" data-dismiss="alert" aria-label="Close">        <span aria-hidden="true">&times;</span>  </button ></div>`;
    element.innerHTML = html;

    alertFadeFunc();
});

connection.start();