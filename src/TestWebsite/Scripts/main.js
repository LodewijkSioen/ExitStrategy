$(function() {
    $("#showFramework").change(function() {
        if ($(this).is(":checked")) {
            $(".mvc, .webforms").addClass("showFx");
        } else {
            $(".mvc, .webforms").removeClass("showFx");
        }
    }).prop("checked", false);
});