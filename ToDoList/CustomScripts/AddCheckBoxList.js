$(document).ready(function () {
    $('.ActiveCheck').change(function () {
        var self = $(this);
        var id = self.attr('id');
        var value = self.prop('checked');

        $.ajax({

            url: '/ToDoes/EditIsDoneCheckBox',
            data: {
                id: id,
                value: value
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        });
    });

});