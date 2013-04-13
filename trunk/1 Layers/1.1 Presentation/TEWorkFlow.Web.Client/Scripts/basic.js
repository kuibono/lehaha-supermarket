$(function () {
    $(".textbox").focus(function () {
        $(this).parent().find(".placeholder").hide();
    }).blur(function () {
        if ($(this).val().length == 0) {
            $(this).parent().find(".placeholder").show();
        }
    });
    //pre load a shadow div and a loaing img div at the top of the BODY of document
    $(document.body).prepend('<div class="shadow"></div> <div class="cover"><p id="loading_msg">Loading...</p><p>&nbsp;</p><img src="/images/ajax-loader.gif" /></div>');

    $("form").submit(function () {
        loading("正在处理，请稍候...");
    });

    $(".textbox").each(function () {
        if ($(this).val().length > 0) {
            $(this).parent().find(".placeholder").hide();
        }
    });

    setTimeout(function () {
        $(".textbox").each(function () {
            if ($(this).val().length > 0) {
                $(this).parent().find(".placeholder").hide();
            }
        });
    }, "500");

    //textarea autoheight
    $("textarea.autoheight").bind("paste cut keydown keyup focus blur", function () {
        var maxHeight = 500;
        var minHeight = 150;
        var height, style = this.style;
        this.style.height = minHeight + 'px';
        if (this.scrollHeight > minHeight) {
            if (maxHeight && this.scrollHeight > maxHeight) {
                height = maxHeight;
                style.overflowY = 'scroll';
            } else {
                height = this.scrollHeight;
                style.overflowY = 'hidden';
            }
            style.height = height + 'px';
        }
    })

    //textbox padding
    $(".textbox").each(function () {
        $(this).width($(this).parent().width() - 20);
        $(this).show();
    });

    //ul.listmenu li
//    $("ul.listmenu li").each(function () {
//        $(this).width($(this).parent().width() / 2 - 5);
//        $(this).show();
//    });

//    $(window).bind("resize", function () {
//        $(".textbox").each(function () {
//            $(this).width($(this).parent().width() - 20);
//            $(this).show();
//        });
//        $("ul.listmenu li").each(function () {
//            $(this).width($(this).parent().width() / 2 - 5);
//            $(this).show();
//        });
//    })
})
function loading(msg) {
    if (msg) {
        $("#loading_msg").text(msg);
    }
    $(".shadow").show();
    $(".cover").show();
}
function endloading() {
    $(".shadow").hide();
    $(".cover").hide();
}