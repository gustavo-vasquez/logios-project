$(document).ready(function () {
    if ($("Notification").show()) {
        $("#Notification").fadeTo(4000, 500).slideUp(500, function () {
            $("#Notification").alert('close');
        })
    }
});