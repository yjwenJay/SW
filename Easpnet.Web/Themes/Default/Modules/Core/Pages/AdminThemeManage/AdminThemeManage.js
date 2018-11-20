$(function() {
    $("input[type='submit']").click(function() {
        $("input[type='radio']").removeAttr("checked");
        $("input#theme-" + $(this).attr("fortheme")).attr("checked", "checked");
    });
});