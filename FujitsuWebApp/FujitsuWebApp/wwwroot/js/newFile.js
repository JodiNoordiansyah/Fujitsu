$("[id*=tSupplier] input[type=checkbox]").click(function() {
    if ($(this).is(":checked")) {
        $("[id*=tSupplier] input[type=checkbox]").removeAttr("checked");
        $(this).attr("checked", "checked");
    }
});
