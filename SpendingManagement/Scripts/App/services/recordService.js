
var RecordService = function () {
    var deleteRecord = function (recordId, done, fail) {
        $.ajax({
            url: "/api/record/DeleteRecord/" + recordId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deleteRecord: deleteRecord
    }
}();