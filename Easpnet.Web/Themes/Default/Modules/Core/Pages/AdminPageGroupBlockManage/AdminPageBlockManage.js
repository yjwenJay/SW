$(function() {
    $(".column").sortable({
        connectWith: ".column",
        items: "div.portlet"
    });

    $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
		.find(".portlet-header")
			.addClass("ui-widget-header ui-corner-all")
			.prepend("<span class='ui-icon ui-icon-minusthick'></span>")
			.end()
		.find(".portlet-content");

    $(".portlet-header .ui-icon").click(function() {
        $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
        $(this).parents(".portlet:first").find(".portlet-content").toggle();
    });

    $(".column").disableSelection();

    // BUTTON
    //	$('.fg-button').hover(
    //		function(){ $(this).removeClass('ui-state-default').addClass('ui-state-focus'); },
    //		function(){ $(this).removeClass('ui-state-focus').addClass('ui-state-default'); }
    //	);

    // MENU	
    $('#flat').menu({
        content: $('#flat').next().html(), // grab content from this page
        showSpeed: 0
    });


    //delete_block
    $(".delete_block").click(function() {
        $(this).parents("div.portlet").remove();
    });

    //save_page
    $("#save_page").click(function() {
        //
        $("div[btype]").each(function() {
            $(this).find("input[type='hidden']").attr("name", $(this).attr("btype"));
        });

        //
        $("#form-main").ajaxSubmit({
            dataType: "json",
            success: function(msg) {
                if (msg.success == "True") {
                    alert("保存成功");
                }
                else {
                    alert(unescape(msg.error));
                }
            }
        });
    });

    //edit_column_visibility
    $(".edit_column_visibility").click(function() {
        var btype = $(this).parents("div['btype']").attr("btype");
        var visibility = $(this).attr("action");

        $.ajax({
            url: $("#form-main").attr("action"),
            type: "post",
            dataType: "json",
            data: "action=edit_column_visibility&btype=" + btype + "&act=" + visibility,
            success: function(msg) {
                if (msg.success == "True") {
                    window.location = unescape(msg.info);
                }
                else {
                    alert(unescape(msg.error));
                }
            }
        });

    });

});


