// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var connection = new signalR.HubConnectionBuilder().withUrl("/userNotificationHub").build();

connection.on("ReceiveMessageSuccess", function (message) {
    document.getElementById("alertPanel").innerHTML = message
    document.getElementById("alertPanel").style.display = "block"
});

connection.start();