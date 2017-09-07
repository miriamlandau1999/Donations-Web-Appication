$(function(){
    $(".amount").keyup(function () {
        $(".submit").prop('disabled', !$(this).val());
    });
});

