$(function () {
    $("#map-side-bar").sortable({
        placeholder: "ui-state-highlight"
    });
    $("#map-side-bar").disableSelection();

    $('.delete-form').live("submit", function () {
        var action = $(this).attr('action');
        var queryString = {};
        action.replace(
            new RegExp("([^?=&]+)(=([^&]*))?", "g"),
            function ($0, $1, $2, $3) { queryString[$1] = $3; }
        );
        var parcelId = queryString["parcelId"];
        //TODO var name = $.trim($(".info-box-name", ui.unselected).text());
        $.whitney.queue.remove(parcelId);
        propertyViewModel.queued.remove({ "ParcelId": parcelId, "Name": name });
        dtable.fnDeleteRow($(this).parents('tr'));
        return false;
    });

    var dtable = $('.property-tbl').dataTable({
        "aoColumns":
            [
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                { "bSortable": false }
            ],
        "aaSorting": [[12, "desc"]],
        "bFilter": false,
        "bPaginate": false
    });
});


// Queue view model
var cookieQueued = jQuery.parseJSON($.cookie('ws')) ? jQuery.parseJSON($.cookie('ws')).QueuedItems : [];
var propertyViewModel = {
    queued: ko.observableArray(cookieQueued),
    remove: function (item) {
        this.queued.remove(item);
    }
};

// Initialise Queue ViewModel
$(function () {
    ko.applyBindings(propertyViewModel, $('.property-tbl')[0]);
});


// model helpers
$.extend($.whitney, {
    queue: {
        url: {},
        urls: function (a) {
            $.extend($.whitney.queue.url, a);
        },
        managers: {},
        clearResults: function (callback) {
            $("#map-side-bar").empty();
            if (callback != undefined) {
                callback();
            }
        },
        remove: function (id) {
            $.ajax({
                url: $.whitney.queue.url.remove,
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify({ parcelId : id }),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
//                    $.whitney.queue.clearResults();
//                    $("#propertyItemTemplate").tmpl(data).appendTo($("#map-side-bar"));
                },
                error: function () {
                }
            });
        }
    }
});