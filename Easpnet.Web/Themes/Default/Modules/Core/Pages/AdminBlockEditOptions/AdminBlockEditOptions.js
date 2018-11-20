$(function() {
    $("form#formEditOption").submit(function() {
        $(this).ajaxSubmit({
            dataType: "json",
            success: function(msg) {
                if (msg.success == "True") {

                }
                else {
                    alert(unescape(msg.error));
                }
            }
        });
        return false;
    });

    //delete_option
    $(".delete_option").click(function() {
        if (!confirm($(this).attr("confirmText"))) {
            return;
        }

        $.ajax({
            url: $("form#formEditOption").attr("action"),
            type: "post",
            dataType: "json",
            data: "action=delete_option&key=" + $(this).parents("tr").attr("key"),
            success: function(msg) {
                if (msg.success == "True") {

                }
                else {
                    alert(unescape(msg.error));
                }
            }
        });
    });

    //edit_option
    $(".edit_option").click(function() {
        $.ajax({
            url: $("form#formEditOption").attr("action"),
            type: "post",
            dataType: "json",
            data: "action=edit_option&key=" + $(this).parents("tr").attr("key"),
            success: function(msg) {
                $("#hidAction").val("update_option");
                $("#key").attr("readonly", "readonly").addClass("readonly");
                $("#OptionName").val(msg.OptionName);
                $("#key").val(msg.OptionKey);
                $("#InputMethod" + msg.InputMethod).click();
                //description
                $("#values").val("");
                var i = 0;
                for (i = 0; i < msg.Values.length;  i++){
                    $("#values").val($("#values").val() + msg.Values[i].Name + "=" + msg.Values[i].Value + "\n");
                }
                
            }
        });
    });

});