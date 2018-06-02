
var RecordController = function (recordService) {
    var deleteButton;

    var init = function () {
        $(".js-delete-expense").click(checkBox);
    };
    var checkBox = function (e) {
        deleteButton = $(e.target);
        bootbox.confirm({
            message: "Czy na pewno chcesz usunąć rekord?",
            buttons: {
                confirm: {
                    label: 'Tak',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Nie',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result) {
                    recordService.deleteRecord(deleteButton.attr("data-expense-id"), done, fail);
                }
            }
        });
    };

    var fail = function () {
        alert("Coś poszło nie tak");
    };
    var done = function () {
        deleteButton.parents('tr').fadeOut(1000, function () {
            $(this).remove();
        });
    };

    return {
        init: init
    };
}(RecordService);
