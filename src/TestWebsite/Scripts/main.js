$(function() {
    $("#showFramework").change(function() {
        if ($(this).is(":checked")) {
            $(".mvc, .webforms").css("borderStyle", "solid");
        } else {
            $(".mvc, .webforms").css("borderStyle", "none");
        }
    }).prop("checked", false);
});