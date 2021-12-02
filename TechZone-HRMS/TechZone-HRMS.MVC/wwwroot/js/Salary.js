var salary = {};
var apiUrl = "https://localhost:5001/api/SalaryAPI";
salary.showData = function () {
    $.ajax({
        url: apiUrl,
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
                    <a href="profile.html">EmployeeName <span>${item.employeeRole}</span></a>
                                </h2>
                            </td>
            <td>FT-0001</td>
            <td>johndoe@example.com</td>
            <td>1 Jan 2013</td>
            <td>
                <div class="dropdown">
                    <a href="" class="btn btn-white btn-sm btn-rounded dropdown-toggle" data-toggle="dropdown" aria-expanded="false">Web Designer </a>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="#">Software Engineer</a>
                        <a class="dropdown-item" href="#">Software Tester</a>
                        <a class="dropdown-item" href="#">Frontend Developer</a>
                        <a class="dropdown-item" href="#">UI/UX Developer</a>
                    </div>
                </div>
            </td>
            <td>$59698</td>
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
