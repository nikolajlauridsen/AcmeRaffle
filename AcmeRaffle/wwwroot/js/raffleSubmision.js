$(document).ready(function () {
    $('#submissionForm').submit(function (e) {
        e.preventDefault();
        $('#submitBtn').prop("disabled", true);
        $('.overlay').show();
        var data = {
            'FirstName': $('#firstName').val(),
            'LastName': $('#lastName').val(),
            'Age': parseInt($('#age').val()),
            'Email': $('#email').val(),
            'SoldProduct': {
                'SerialNumber': $('#serialNumber').val()
            }
        }
        console.log(JSON.stringify(data));

        $.ajax({
            type: "POST",
            url: '/api/RaffleApi',
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                alert("Form successfully posted.");
                $('#submitBtn').prop("disabled", false);
                $('.overlay').hide();
            },
            error: function (e) {
                alert("Form posting failed.");
                $('#submitBtn').prop("disabled", false);
                $('.overlay').hide();
            }
        });
    });
})