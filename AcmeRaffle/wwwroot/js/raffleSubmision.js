$(document).ready(function() {
    $('#submissionForm').submit(function(e) {
        e.preventDefault();
        lockForm();

        var data = packData();
        $.ajax({
            type: "POST",
            url: '/api/RaffleApi',
            data: JSON.stringify(data),
            contentType: "application/json",
            success: handleSuccess,
            error: handleFailure
        });
    });
});

function lockForm() {
    $('#submitBtn').prop("disabled", true);
    $('.overlay').show();
}

function unlockForm() {
    $('#submitBtn').prop("disabled", false);
    $('.overlay').hide();
    $("#submissionForm").trigger('reset');
}

function packData() {
    return {
        'FirstName': $('#firstName').val(),
        'LastName': $('#lastName').val(),
        'Age': parseInt($('#age').val()),
        'Email': $('#email').val(),
        'SoldProduct': {
            'SerialNumber': $('#serialNumber').val()
        }
    }
}

function handleSuccess(data) {
    closeFailure();
    $("#successAlert").show();
    unlockForm();
}

function handleFailure(error) {
    closeSuccess();
    $("#failureAlert").show();
    unlockForm();
}

function closeFailure() {
    $("#failureAlert").hide();
}

function closeSuccess() {
    $("#successAlert").hide();
}