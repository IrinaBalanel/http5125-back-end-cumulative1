
window.onload = function () {
    form.onsubmit = validateForm;

    var form = document.getElementById("TeacherForm");
    var teacherFNameInput = document.getElementById("TeacherFirstName");
    var teacherLNameInput = document.getElementById("TeacherLastName");
    var teacherEmployeeNumberInput = document.getElementById("EmployeeNumber");
    var salaryInput = document.getElementById("Salary");
    var errorMessage = document.getElementById("TeacherFirstName").nextElementSibling;

    function validateForm() {
        if (teacherFNameInput.value === "" || teacherLNameInput.value === "" || teacherEmployeeNumberInput.value === "" || salaryInput.value == 0) {

            console.log(salaryInput.value);
            alert("The data is not inserted into the DB");
            console.log("Status: Unsuccessful");
            return false;

        } else {
            alert("The data has been inserted into the DB");
            console.log("Status: Successful");
        }


    }


    var updateForm = document.getElementById("UpdateTeacherForm");
    var fNameInput = document.getElementById("UpdateTeacherFirstName");
    var lNameInput = document.getElementById("UpdateTeacherLastName");
    var employeeNumberInput = document.getElementById("UpdateEmployeeNumber");
    var newSalaryInput = document.getElementById("UpdateSalary");

    updateForm.onsubmit = validateUpdateForm;
    function validateUpdateForm() {
        console.log(fNameInput)
        if (fNameInput.value == "" || lNameInput.value === "" || employeeNumberInput.value === "" || newSalaryInput.value == 0) {
            console.log(newSalaryInput.value);
            alert("The data is not inserted into the DB");
            console.log("Status: Unsuccessful");
            return false;

        } else {
            alert("The data has been inserted into the DB");
            console.log("Status: Successful");
            return true;
        }


    }

    function UpdateTeacher(TeacherId) {

        //goal: send a request like this:
        //POST : http://localhost:54070/api/TeacherData/UpdateTeacher/{id}
        //with POST data about teacher

        var URL = "http://localhost:54070/api/TeacherData/UpdateTeacher/" + TeacherId;

        var rq = new XMLHttpRequest();

        var TeacherFname = document.getElementById('TeacherFname').value;
        var TeacherLname = document.getElementById('TeacherLname').value;
        var EmployeeNumber = document.getElementById('EmployeeNumber').value;
        var Salary = document.getElementById('Salary').value;


        var TeacherData = {
            "TeTeacherFirstName": TeacherFname,
            "TeacherLastName": TeacherLname,
            "EmployeeNumber": EmployeeNumber,
            "Salary": Salary
        };


        rq.open("POST", URL, true);
        rq.setRequestHeader("Content-Type", "application/json");
        rq.onreadystatechange = function () {

            if (rq.readyState == 4 && rq.status == 200) {
                //request is successful and the request is finished

                //nothing to render, the method returns nothing.


            }

        }
        //POST information sent through the .send() method
        rq.send(JSON.stringify(TeacherData));
    }

}