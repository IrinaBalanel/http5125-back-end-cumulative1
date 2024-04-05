var form = document.getElementById("TeacherForm");
var teacherFNameInput = document.getElementById("TeacherFirstName");
var teacherLNameInput = document.getElementById("TeacherLastName");
var errorMessage = document.getElementById("TeacherFirstName").nextElementSibling;

function validateForm() {
    if (teacherFNameInput.value === "" || teacherLNameInput.value === "") {
        alert("The data is not inserted into the DB");
        console.log("Status: Unsuccessful");
        return false;

    } else {
        alert("The data has been inserted into the DB");
        console.log("Status: Successful");
    }
    
    
}

form.onsubmit = validateForm;