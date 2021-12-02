var salary = {};
var apiUrl = "https://localhost:5001/api/SalaryAPI";
salary.showData = function () {
    $.ajax({
        url: https://localhost:5001/api/EmployeeAPI,
        method: "GET",
        datatype: "json",
        success: function (data) {
            $('#tbSalary>tbody').empty();
            $.each(data, function (index, item) {
                $('#tbSalary>tbody').append(
                    `<tr>
        <td>
            <h2 class="table-avatar">
                <a href="profile.html" class="avatar"><img alt="" src="assets/img/profiles/avatar-02.jpg"></a>
                    <a href="profile.html">${item.firstName} <span></span></a>
                                </h2>
                            </td>
            <td>FT-0001</td>
            <td>${item.email}</td>
            <td>${item.joinDate}</td>
            <td><a class="btn btn-sm btn-primary">Salary</a></td>
            <td><a class="btn btn-sm btn-primary" href="salary-view.html">Generate Slip</a></td>
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
            }

    })
}

