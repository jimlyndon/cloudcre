(function ($, window) {

    // On DOM load
    $(function () {
        
        window.cloudcre.viewModelOnBind = function () {

            $("input.submit-button").click(function (e) {
                e.preventDefault();
                var options = {
                    success: function () {
                        $("#submit-dialog-msg").text("The new property, \"" + cloudcre.viewModel.Name() + "\", was successfully created.");
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