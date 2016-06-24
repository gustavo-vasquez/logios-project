$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('#goTop').goTop({ appear: 20 });

    if ($('.arrowDown') != "undefined") {
        $('.arrowDown').click(function () {            
            $(this).parent().prev().css('height', '+=250');            
        });

        $('.arrowUp').click(function () {
            $(this).parent().prev().css('height', '-=250');            
        });
    }
});