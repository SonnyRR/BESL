// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var connection = new signalR.HubConnectionBuilder().withUrl("/userNotificationHub").build();

function alertFadeFunc() {
    window.setTimeout(function () {
        $(".alert").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
            element.style.display = "hidden";
        });
    }, 4000);

}
connection.on("ReceiveMessageSuccess", function (messageHeader, message) {
    let element = document.getElementById("alertPanel");
    element.style.display = "block";
    let html = `<div id="alertDiv" class="alert alert-success alert-dismissible fade show" role="alert"><strong>${messageHeader}</strong> ${message}<button type = "button" class="close" data-dismiss="alert" aria-label="Close">        <span aria-hidden="true">&times;</span>  </button ></div>`;
    element.innerHTML = html;
    alertFadeFunc();

});

connection.on("ReceiveMessageFailiure", function (messageHeader, message) {
    let element = document.getElementById("alertPanel");
    element.style.display = "block";
    let html = `<div id="alertDiv" class="alert alert-danger alert-dismissible fade show" role="alert"><strong>${messageHeader}</strong> ${message}<button type = "button" class="close" data-dismiss="alert" aria-label="Close">        <span aria-hidden="true">&times;</span>  </button ></div>`;
    element.innerHTML = html;

    alertFadeFunc();
});


connection.start();