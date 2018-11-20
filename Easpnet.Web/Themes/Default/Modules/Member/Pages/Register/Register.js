$(function() {
    $("#submitBn").click(function() {
        $("#regForm").submit();
        return false;
    });

    //提示
//    var tooltipObj = new DHTMLgoodies_formTooltip();
//    tooltipObj.setTooltipPosition('right');
//    tooltipObj.setPageBgColor('#FFF');
//    tooltipObj.setTooltipCornerSize(15);
//    tooltipObj.tooltipCornerSize = 0;
//    tooltipObj.initFormFieldTooltip();
    
    //
    $("#regForm").validationEngine()
});
