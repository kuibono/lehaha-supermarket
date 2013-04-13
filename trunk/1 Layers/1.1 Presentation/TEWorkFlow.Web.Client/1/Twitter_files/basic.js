$(function () {
    $(".textbox").focus(function () {
        $(this).parent().find(".placeholder").hide();
    }).blur(function () {
        if ($(this).val().length == 0) {
            $(this).parent().find(".placeholder").show();
        }
    });
})