(function ($, window) {

    // On DOM load
    $(function () {

        window.cloudcre.viewModelOnBind = function () {
            cloudcre.App.createMap();

            $("input.submit-button").click(function (e) {
                e.preventDefault();
                var options = {
                    success: function () {
                        $("#submit-dialog-msg").text("The property, \"" + cloudcre.viewModel.Name() + "\", was successfully saved.");
                        $("#submit-dialog").dialog("option", "buttons", {
                            Ok: function () {
                                $(this).dialog("close");
                                window.opener.displayPage();
                                window.self.close();
                            }
                        });
                        $("#submit-dialog").dialog("open");
                    },
                    url: cloudcre.routing.url.action
                };

                $.validator.unobtrusive.parse($("#dialog-form"));
                if ($("#dialog-form").valid()) {
                    $("#dialog-form").ajaxSubmit(options);
                }
            });
        };

    });
})(jQuery, window);