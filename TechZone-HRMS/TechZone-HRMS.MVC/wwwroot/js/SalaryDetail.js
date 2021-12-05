var salary = {};
var apiUrl = "https://localhost:5001/api/SalaryAPI";
salary.save = function() {
    if($('#formSalary').valid()) {
        if (Number($('#salaryId').val()) == 0) {
            let createSalObj = {
                employeeId: $('#employeeId').val(),
                labourContractSalary: $('#labourContractSalary').val(),
                monthsWorkday: $('#monthsWorkday').val(),
                totalWorkday: $('#totalWorkday').val(),
                lunchAllowance: $('#lunchAllowance').val(),
                mobilePhoneAllowance: $('#mobilePhoneAllowance').val(),
                conveyanceAllowance: $('#conveyanceAllowance').val(),
                performanceBonus: $('#performanceBonus').val()
            };
            $.ajax({
                url: apiUrl,
                method: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(createSalObj),
                success: function (data) {
                    if (data.success) {
                        $('#add_salary').modal('hide');
                        $.notify("Salary has been created successfully!", "success");
                        location.reload(true);
                    }
                    else {
                        $.notify("Something went wrong, please try again!", "error");
                    }
                }
            });
        } else {
            let updateSalObj = {
                salaryId: Number($('#salaryId').val()),
                employeeId: $('#employeeId').val(),
                labourContractSalary: $('#labourContractSalary').val(),
                monthsWorkday: $('#monthsWorkday').val(),
                totalWorkday: $('#totalWorkday').val(),
                lunchAllowance: $('#lunchAllowance').val(),
                mobilePhoneAllowance: $('#mobilePhoneAllowance').val(),
                conveyanceAllowance: $('#conveyanceAllowance').val(),
                performanceBonus: $('#performanceBonus').val()
            };
            $.ajax({
                url: apiUrl,
                method: "PUT",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(updateSalObj),
                success: function (data) {
                    if (data.success) {
                        $('#add_salary').modal('hide');
                        $.notify("Salary has been edited successfully!", "success");
                        location.reload(true);
                    }
                    else {
                        $.notify("Something went wrong, please try again!", "error");
                    }
                }
            })
        }
    }
}

salary.reset = function() {
    $('#salaryId').val(0),
    $('#employeeId').val(),
    $('#labourContractSalary').val(""),
    $('#monthsWorkday').val(""),
    $('#totalWorkday').val(""),
    $('#lunchAllowance').val(""),
    $('#mobilePhoneAllowance').val(""),
    $('#conveyanceAllowance').val(""),
    $('#performanceBonus').val(""),
    $('#add_salary').find(".modal-title").text("Add Staff Salary");
}

salary.openModal = function() {
    $('#add_salary').modal('show');
    salary.reset();
}

salary.getSalbySalId = function(id) {
    $.ajax({
        url: `${apiUrl}/${id}`,
        method: "GET",
        datatype: "json",
        success: function (data) {
            $('#employeeId').val(data.employeeId),
            $('#labourContractSalary').val(data.labourContractSalaryId),
            $('#monthsWorkday').val(data.monthsWorkday),
            $('#totalWorkday').val(data.totalWorkday),
            $('#lunchAllowance').val(data.lunchAllowance),
            $('#mobilePhoneAllowance').val(data.mobilePhoneAllowance),
            $('#conveyanceAllowance').val(data.conveyanceAllowance),
            $('#performanceBonus').val(data.performanceBonus),
            $('#add_salary').find(".modal-title").text("Edit Staff Salary");
            $('#add_salary').modal('show');
        }
    })
}