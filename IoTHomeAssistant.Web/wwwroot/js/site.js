"use strict";

function setupDeviceNotification(topicId) {

    var connection = getHubConnection(topicId);

    connection.on("ReceiveData", (payload) => {
        var element = $(".device-topic-" + topicId);
        if (element != undefined) {
            if (!element.hasClass("fade-in")) {
                element.addClass("fade-in");
            }

            element.text(payload);
        }
    });

    startHubConnection(connection, topicId);
}


function setupSwitchNotification(topicId) {

    var connection = getHubConnection(topicId);

    connection.on("ReceiveData", (payload) => {
        $("input.switch-" + topicId).prop('checked', payload == 'ON' || payload == 1 || payload == true);
    });

    startHubConnection(connection, topicId);
}

function getHubConnection(topicId) {
    return new signalR
        .HubConnectionBuilder()
        .withUrl("/DeviceHub?topicId=" + topicId)
        .build();
}

function startHubConnection(connection, topicId) {
    connection.start()
        .catch(function (err) {
            return console.error(err.toString());
        }).then(function () {
            connection.invoke("GetConnectionId", topicId);
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

    $("input[type=checkbox].toggle").click(function () {
        var input = $(this);

        $.ajax({
            type: "POST",
            url: "/api/devices/toggle",
            data: JSON.stringify({
                id: input.attr('data-id'),
                toggle: input.prop('checked')
            }),
            dataType: 'json',
            contentType: 'application/json',
        });
    });
});