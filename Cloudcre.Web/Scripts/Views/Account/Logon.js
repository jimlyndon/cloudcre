//Event bindings for watermark effects
$(document).ready(function () {
    $('.inputWrapper > :input').bind('click focus', function () { $(this).parent().addClass('blurred'); });
    $('.inputWrapper > :input').bind('focusout', function () { $(this).parent().removeClass('blurred'); });
    $('.inputWrapper > :input').bind('keyup change', function () { refreshInput($(this)); });
    refreshInput($('.inputWrapper > :input'));
});

function refreshInput(obj) {
    obj.each(function () {
        if ($(this).val() != "") {
            $(this).parent().addClass('filled');
        } else {
            $(this).parent().removeClass('filled');
        }
    });
}