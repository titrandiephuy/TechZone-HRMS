var employee = {};
var apiUrl = "https://localhost:5001/api/EmployeeAPI";
employee.ShowData = function () {
  $.ajax({
    url: apiUrl,
    method: "GET",
    dataType: "json",
    success: function (data) {
      $("#tbEmployee>tbody").empty();
      $.each(data, function (index, item) {
        $("#tbEmployee>tbody").append(
          `   <tr>
                            <td>${item.employeeId}</td>
                            <td>
                                <h2 class="table-avatar">
                                    <a href="profile.html" class="avatar"><img alt="" src="~/assets/img/profiles/avatar-02.jpg"></a>
                                    <a href="profile.html">${item.firstName}</a>
                                </h2>
                            </td>
                            <td>${item.email}</td>
                            <td>${item.employeePhoneNumber}</td>
                            <td>${item.departmentName}</td>
                            <td>
                                <span class="badge ${
                                  item.employeeStatus
                                    ? "bg-success"
                                    : "bg-danger"
                                }">
                                ${item.employeeStatus ? "active" : "inactive"}
                                </span>
                            </td>
                            <td class="text-right">
                                <div class="dropdown dropdown-action">
                                    <a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <a class="dropdown-item" href="javascript:;" onclick='employee.getEmpById(${item.employeeId})'><i class="fa fa-pencil m-r-5"></i> Edit</a>
                                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#delete_employee"><i class="fa fa-edit m-r-5"></i> Detail</a>
                                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#delete_employee"><i class="fa fa-trash-o m-r-5"></i> Delete</a>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    `
        );
      });
      $.fn.dataTable.ext.errMode = "none";
      $("#tbEmployee").DataTable();
    },
  });
};

employee.save = function () {
  if ($("#formEmployee").valid()) {
    if (Number($("#employeeId").val()) == 0) {
      let createObj = {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        gender: $("#gender").val(),
        employeePhoneNumber: $("#employeePhoneNumber").val(),
        email: $("#email").val(),
        employeeAddress: $("#employeeAddress").val(),
        dateOfBirth: $("#dateOfBirth").val(),
        placeOfOrigin: $("#placeOfOrigin").val(),
        ethnicity: $("#ethnicity").val(),
        employeeAvatar: $("#employeeAvatar").val(),
        departmentId: $("#departmentId").val(),
        educationLevelId: $("#educationLevelId").val(),
        employeeStatus: $("#employeeStatus").val(),
      };
      $.ajax({
        url: apiUrl,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(createObj),
        success: function (data) {
          if (data.success) {
            $("#add_employee").modal("hide");
            $.notify("Employee has been created successfully!", "success");
            employee.ShowData();
          } else {
            $.notify("Something went wrong, please try again!", "error");
          }
        },
      });
    } else {
      let updateObj = {
        employeeId: Number($("#employeeId").val()),
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        gender: $("#gender").val(),
        employeePhoneNumber: $("#employeePhoneNumber").val(),
        email: $("#email").val(),
        employeeAddress: $("#employeeAddress").val(),
        dateOfBirth: $("#dateOfBirth").val(),
        placeOfOrigin: $("#placeOfOrigin").val(),
        ethnicity: $("#ethnicity").val(),
        employeeAvatar: $("#employeeAvatar").val(),
        departmentId: $("#departmentId").val(),
        educationLevelId: $("#educationLevelId").val(),
        employeeStatus: $("#employeeStatus").val(),
      };
      $.ajax({
        url: apiUrl,
        method: "PUT",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(updateObj),
        success: function (data) {
          if (data.success) {
            $("#add_employee").modal("hide");
            $.notify("Employee has been edited successfully!", "success");
            employee.ShowData();
          } else {
            $.notify("Something went wrong, please try again!", "error");
          }
        },
      });
    }
  }
};

employee.getEmpById = function (id) {
  $.ajax({
    url: `${apiUrl}/GetEmployeeByIdShow/${id}`,
    method: "GET",
    datatype: "json",
    success: function (employee) {
      $("#firstName").val(employee.firstName);
      $("#lastName").val(employee.lastName);
      $("#gender").val(employee.gender);
      $("#employeePhoneNumber").val(employee.employeePhoneNumber);
      $("#email").val(employee.email);
      $("#employeeAddress").val(employee.employeeAddress);
      $("#dateOfBirth").val(employee.dateOfBirth);
      $("#placeOfOrigin").val(employee.placeOfOrigin);
      $("#ethnicity").val(employee.ethnicity);
      $("#employeeAvatar").val(employee.employeeAvatar);
      $("#departmentId").val(employee.departmentId);
      $("#educationLevelId").val(employee.educationLevelId);
      $("#employeeStatus").val(employee.employeeStatus);
      $('#employeeId').val(employee.employeeId);
      $('#add_employee').find(".modal-title").text("Update Employee");
      $('#add_employee').modal('show');
    },
  });
};
employee.reset = function () {
  $("#formEmployee").validate().resetForm();
  $("#firstName").val("");
  $("#lastName").val("");
  $("#employeePhoneNumber").val("");
  $("#email").val("");
  $("#employeeAddress").val("");
  $("#dateOfBirth").val("");
  $("#placeOfOrigin").val("");
  $("#ethnicity").val("");
  $("#employeeAvatar").val("");
  $("#employeeId").val(0);
  $("#educationLevelId").val(0);
  $("#employeeStatus").val("");
};
employee.openModal = function () {
  $("#add_employee").modal("show");
  employee.reset();
};

$(document).ready(function () {
  employee.ShowData();
});
