$(function () {
    $(".confirm, .password").keyup(function () {
        var password = $(".password").val();
        var confirm = $(".confirm").val();
        $(".signUp").prop('disabled', !PasswordMatch(password, confirm));
    });
});

function PasswordMatch(password, confirm) {
    return password === confirm;
}