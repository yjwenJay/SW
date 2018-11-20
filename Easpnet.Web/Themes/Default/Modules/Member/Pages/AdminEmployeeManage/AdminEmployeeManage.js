$(function() {
    //删除
    $(".btnDelete").click(function() {
        if(confirm("删除的数据将不能恢复，您确定要删除该用户？")){
            $("input[name='ids']").removeAttr("checked");
            $(this).parents("tr").find("input[name='ids']").attr("checked", "checked");
            $("#option").val("Delete");
            $("form#formList").submit();
        }
    });
    //解绑
    $(".unbind").click(function() {
        if(confirm("确定解除物理地址绑定吗？")){
            $("input[name='ids']").removeAttr("checked");
            $(this).parents("tr").find("input[name='ids']").attr("checked", "checked");
            $("#option").val("unbind");
            $("form#formList").submit();
        }
    });
});