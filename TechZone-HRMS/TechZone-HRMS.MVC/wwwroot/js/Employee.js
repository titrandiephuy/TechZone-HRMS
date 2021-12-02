var employee = {};
var apiUrl = "https://localhost:5001/api/EmployeeAPI";
employee.ShowData = function () {
    $.ajax({
        url: apiUrl,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('#tbEmployee>tbody').empty();
            $.each(data, function (index, item) {
                $('#tbEmployee>tbody').append(
                    `   <tr>
                            <td>${item.employeeId}</td>
                            <td>
                                <h2 class="table-avatar">
                                    <a href="profile.html" class="avatar"><img alt="" src="assets/img/profiles/avatar-02.jpg"></a>
                                    <a href="profile.html">${item.firstName}</a>
                                </h2>
                            </td>
                            <td>${item.email}</td>
                            <td>${item.employeePhoneNumber}</td>
                            <td>${item.departmentName}</td>
                            <td>
                                <span class="badge ${item.employeeStatus ? 'bg-success' : 'bg-danger'}">
                                ${item.employeeStatus ? 'active' : 'inactive'}
                                </span>
                            </td>
                            <td class="text-right">
                                <div class="dropdown dropdown-action">
                                    <a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#edit_employee" onclick='employee.getEmpById(${item.employeeId})'><i class="fa fa-pencil m-r-5"></i> Edit</a>
                                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#delete_employee"><i class="fa fa-edit m-r-5"></i> Detail</a>
                                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#delete_employee"><i class="fa fa-trash-o m-r-5"></i> Delete</a>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    `
                );
            });
            $.fn.dataTable.ext.errMode = 'none';
            $('#tbEmployee').DataTable();
        }
    })
}


employee.save = function () {
    let createObj = {
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        gender: $('#gender').is(':checked'),
        employeePhoneNumber: $('#employeePhoneNumber').val(),
        email: $('#email').val(),
        employeeAddress: $('#employeeAddress').val(),
        dateOfBirth: $('#dateOfBirth').val(),
        placeOfOrigin: $('#placeOfOrigin').val(),
        ethnicity: $('#ethnicity').val(),
        employeeAvatar: $('#employeeAvatar').val(),
        departmentId: $('#departmentId').val(),
        salaryId: $('#salaryId').val(),
        educationLevelId: $('#educationLevelId').val(),
        employeeStatus: $('#employeeStatus').is(':checked'),
    };
    $.ajax({
        url: apiUrl,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(createObj),
        success: function (data) {
            if (data.success) {
                $('#add_employee').modal('hide');
                $.notify("Employee has been created successfully!", "success");
                employee.ShowData();
            }
            else {
                $.notify("Something went wrong, please try again!", "error");
            }
        }
    })
}

employee.getEmpById = function (id) {
    $.ajax({
        url: `${apiUrl}/${id}`,
        method: "GET",
        datatype: "json",
        success: function (employee) {
            $('#firstName').val(employee.firstName);
            $('#lastName').val(employee.lastName);
            $('#gender').prop('checked', employee.gender);
            $('#employeePhoneNumber').val(employee.employeePhoneNumber);
            $('#email').val(employee.email);
            $('#employeeAddress').val(employee.employeeAddress);
            $('#dateOfBirth').val(employee.dateOfBirth);
            $('#placeOfOrigin').val(employee.placeOfOrigin);
            $('#ethnicity').val(employee.ethnicity);
            $('#employeeAvatar').val(employee.employeeAvatar);
            $('#departmentId').val(employee.departmentId);
            $('#salaryId').val(employee.salaryId);
            $('#educationLevelId').val(employee.educationLevelId);
            $('#employeeStatus').prop('checked', employee.employeeStatus);
        }
    });
}

$(document).ready(function () {
    employee.ShowData();
})