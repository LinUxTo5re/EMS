$(document).ready(function () {
    $('#submitBtn').on('click', function () {
        $.ajax({
            url: $('#AddEmployeeForm').attr('action'),
            type: 'POST',
            data: $('#AddEmployeeForm').serialize(),
            success: function (response) {
                var IsError = response.IsError;
                var messageText = response.Message;

                if (IsError) {
                    console.log('Error occurred:', messageText);
                } else {
                    console.log('Success:', messageText);
                }
                displayModal(IsError, messageText);
            }
        });
    });
    $('#resetBtn').on('click', function (e) {
        $('#AddEmployeeForm')[0].reset();
        $('#emailError').hide();
    });

    $('#resetBtn').on('click', function () {
        $('#yourForm')[0].reset();
    });

    $('#ContactNumber').on('input', function () {
        var contactNumber = $(this).val().replace(/[^0-9]/g, '').slice(0, 10);
        $(this).val(contactNumber);
    });
    $('#EmployeeName').on('input', function () {
        handleSubmitBtn();
    });
    $('#EmployeeCode').on('input', function () {
        var empCode = $(this).val().replace(/[^0-9]/g, '').slice(0, 6);
        $(this).val(empCode);
        handleSubmitBtn();
    });

    var isEmailFieldBlurred = false;

    $('#Email').on('blur', function () {
        isEmailFieldBlurred = true;
        validateEmail();
    });

    $('#Email').on('input', function () {
        if (isEmailFieldBlurred) {
            validateEmail();
        }
    });

    function validateEmail() {
        var email = $('#Email').val();
        var emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;
        handleSubmitBtn();

        if (!emailRegex.test(email)) {
            $('#emailError').show();
        } else {
            $('#emailError').hide();
        }
    }
    function handleSubmitBtn() {
        var emailValue = $('#Email').val();
        var empcodeValue = $('#EmployeeCode').val();
        var empNameValue = $('#EmployeeName').val();

        if (emailValue === '' || empcodeValue === '' || empNameValue === '') {
            $('#submitBtn').prop('disabled', true);
        } else {
            $('#submitBtn').prop('disabled', false);
        }
    }


    function displayModal(IsError, messageText) {
        var status = IsError ? "Error" : "Information";
        $("#message-popup .modal-title").html(status);
        if (IsError) {
            $("#message-popup .modal-body").html(messageText);
        } else {
            $("#message-popup .modal-body").html(messageText);
        }
        $("#message-popup").modal("show");
    }
});