$(document).ready(function () {
    $.ajax({
        url: '/ToDoes/BuildToDoTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    });
});