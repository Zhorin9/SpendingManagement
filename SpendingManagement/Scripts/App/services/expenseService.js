
var ExpenseService = function () {
    var deleteExpense = function (expenseId, done, fail) {
        $.ajax({
            url: "/api/expense/" + expenseId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deleteExpense: deleteExpense
    }
}();