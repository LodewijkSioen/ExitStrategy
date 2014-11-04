$.fn.datepicker.defaults.format = "dd/mm/yyyy";

$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-block',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    }
});

$(function () {
    $("#showFramework").change(function() {
        if ($(this).is(":checked")) {
            $(".mvc, .webforms").addClass("showFx");
        } else {
            $(".mvc, .webforms").removeClass("showFx");
        }
    }).prop("checked", false);

    $("#disableValidation").change(function() {
        if ($(this).is(":checked")) {
            $("input:submit").addClass("cancel");
        } else {
            $("input:submit").removeClass("cancel");
        }
        
    });
});