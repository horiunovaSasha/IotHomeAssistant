"use strict";

function setupDeviceNotification(deviceId) {

    var connection = new signalR
        .HubConnectionBuilder()
        .withUrl("/DeviceHub?deviceId=" + deviceId)
        .build();

    connection.on("ReceiveData", (payload) => {
        var element = $("#device-topic-" + deviceId);
        if (element != undefined) {
            element.text(payload);
        }

        console.log(deviceId);
    });

    connection.start()
        .catch(function (err) {
            return console.error(err.toString());
        }).then(function () {
            connection.invoke("GetConnectionId", deviceId);
        });
}



$(document).ready(function () {
    $(".form-wrapper .button").click(function () {
        var button = $(this);
        var currentSection = button.parents(".section");
        var currentSectionIndex = currentSection.index();
        var headerSection = $('.steps li').eq(currentSectionIndex);
        currentSection.removeClass("is-active").next().addClass("is-active");
        headerSection.removeClass("is-active").next().addClass("is-active");

        $(".form-wrapper").submit(function (e) {
            e.preventDefault();
        });

        if (currentSectionIndex === 3) {
            $(document).find(".form-wrapper .section").first().addClass("is-active");
            $(document).find(".steps li").first().addClass("is-active");
        }
    });
});