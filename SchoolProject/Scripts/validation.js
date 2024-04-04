var form = document.getElementById("TeacherForm");
var teacherFNameInput = document.getElementById("TeacherFirstName");
var teacherLNameInput = document.getElementById("TeacherLastName");

function validateForm() {
    if (teacherFNameInput.value === "" || teacherLNameInput.value === "") {
        alert("Please enter the required information");
        validateForm.preventDefault();
    }
}

form.onsubmit = validateForm;