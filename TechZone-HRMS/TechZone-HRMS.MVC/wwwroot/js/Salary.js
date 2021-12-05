var salary = {};
var employeeId = 0;
var apiUrl = "https://localhost:44360/api/SalaryAPI";
salary.showData = function () {
  $.ajax({
      url: "https://localhost:44360/api/EmployeeAPI",
    method: "GET",
    datatype: "json",
    success: function (data) {
      $("#tbSalary>tbody").empty();
      $("#tbSalary thead").empty();
      $("#tbSalary thead").append(
          `
          <tr>
          <th>Employee ID</th>
          <th>Employee</th>
          <th>Email</th>
          <th>Joined Date</th>
          <th>Salary</th>
          <th class="text-right">Action</th>
      </tr>
      `
      );
      $.each(data, function (index, item) {
        $("#tbSalary>tbody").append(
          ` <tr>
            <td>${item.employeeId}</td>
            <td>
            <h2 class="table-avatar">
                <a href="profile.html" class="avatar"><img alt="" src="assets/img/profiles/avatar-02.jpg"></a>
                    <a href="profile.html">${item.firstName} <span></span></a>
                                </h2>
            </td>
            <td>${item.email}</td>
            <td>${item.joinDate}</td>
            <td><a class="btn btn-sm btn-primary" href="/Salary/SalaryDetail/${item.employeeId}">View Salary</a></td>
            <td class="text-right">
                <div class="dropdown dropdown-action">
                    <a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
                    <div class="dropdown-menu dropdown-menu-right">
                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#edit_salary"><i class="fa fa-pencil m-r-5"></i> Edit</a>
                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#delete_salary"><i class="fa fa-trash-o m-r-5"></i> Delete</a>
                    </div>
                </div>
            </td>
      </tr>`
        );
      });
      $.fn.dataTable.ext.errMode = "none";
      $("#tbSalary").DataTable();
    },
  });
};

salary.getSalarybyEmpId = function (id) {
  employeeId = id;
  $.ajax({
      url: `https://localhost:44360/api/SalaryAPI/${id}`,
    method: "GET",
    success: function (data) {
      $("tbSalary thead").empty();
      $("tbSalary tbody").empty();
      $("tbSalary thead").append(
        `
                <tr>
                <th>Employee ID</th>
                <th>Employee</th>
                <th>Email</th>
                <th>Salary</th>
                <th>Payslip</th>
                <th class="text-right">Action</th>
                </tr>
                `
      );
      $.each(data, function(index, item) {
        $("tbSalary tbody").append(
            `<tr>
            <td>${item.employeeId}</td>
            <td>
            <h2 class="table-avatar">
                <a href="profile.html" class="avatar"><img alt="" src="assets/img/profiles/avatar-02.jpg"></a>
                    <a href="profile.html">âsasasas</a> <span></span></a>
                                </h2>
            </td>
            <td></td>
            <td>${item.netSalary}</td>
            <td><a class="btn btn-sm btn-primary">Generate Payslip</a></td>
            <td class="text-right">
                <div class="dropdown dropdown-action">
                    <a href="#" class="action-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
                    <div class="dropdown-menu dropdown-menu-right">
                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#edit_salary"><i class="fa fa-pencil m-r-5"></i> Edit</a>
                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#delete_salary"><i class="fa fa-trash-o m-r-5"></i> Delete</a>
                    </div>
                </div>
            </td>
      </tr>`
        )
      })
    },
  });
};
$(document).ready(function () {
  salary.showData();
});
