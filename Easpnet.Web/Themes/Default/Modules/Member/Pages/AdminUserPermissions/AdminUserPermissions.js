$(function() {
    $("form#formSave").submit(function() {
        var list = treeboxbox_tree.getAllChecked();
        alert(list);
        return false;
        $("input[name='values']").val(list);
    });
});