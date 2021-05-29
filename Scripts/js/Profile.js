
var firstnameInput;
var lastnameInput;
var passwordInput;
var phoneInput;
var submitButton;

function initialize() {
    firstnameInput = document.getElementById("firstnameInput");
    lastnameInput = document.getElementById("lastnameInput");
    phoneInput = document.getElementById("phoneInput");
    passwordInput = document.getElementById("passwordInput");
    submitButton = document.getElementById("submitButton");

    firstnameInput.addEventListener("input", function () { validateFirstname(this) });
    lastnameInput.addEventListener("input", function () { validateLastname(this) });
    passwordInput.addEventListener("input", function () { validatePassword(this) });
    phoneInput.addEventListener("input", function () { validatePhone(this) });
    checkUsername();
}

function checkUsername() {
    tr = document.getElementById("usernameTR");
    if (document.getElementById("username").innerHTML == null || document.getElementById("username").innerHTML.length == 0) {
        tr.setAttribute("style", "display:none");
    }
}

function validateFirstname(input) {
    input.classList.remove('is-invalid')
    input.classList.remove('is-valid')
    if (input.value != null && input.value.length > 2)
        input.classList.add('is-valid')
    else
        input.classList.add('is-invalid')
}

function validateLastname(input) {
    input.classList.remove('is-invalid')
    input.classList.remove('is-valid')
    if (input.value != null && input.value.length > 2)
        input.classList.add('is-valid')
    else
        input.classList.add('is-invalid')
}

function validatePassword(input) {
    var passReg = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
    input.classList.remove('is-invalid')
    input.classList.remove('is-valid')
    if (passReg.test(input.value))
        input.classList.add('is-valid')
    else
        input.classList.add('is-invalid')
}

function validatePhone(input) {
    input.classList.remove('is-invalid')
    input.classList.remove('is-valid')
    if (input.value.length === 11)
        input.classList.add('is-valid')
    else
        input.classList.add('is-invalid')
}

function handleForm(userViewEntity) {
    document.getElementById("alert").setAttribute("style", "display: none");
    document.getElementById("alert").classList.remove('alert-success');
    document.getElementById("alert").classList.remove('alert-danger');
    if (validateForm()) {
        $.ajax({
            type: "POST",
            url: "/Actor/Update",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FirstName: firstnameInput.value,
                LastName: lastnameInput.value,
                Phone: phoneInput.value.length == 0 ? userViewEntity.Phone : phoneInput.value,
                Password: passwordInput.value.length == 0 ? userViewEntity.Password : passwordInput.value,
                Email: userViewEntity.Email,
            }),
            success: function (response) {
                document.getElementById("alert").innerText = "Your profile is updated";
                document.getElementById("alert").classList.add('alert-success');
                document.getElementById("alert").removeAttribute("style");
                userViewEntity = response;
                addNewInfo(userViewEntity);
            }
        }).fail(function(){
            document.getElementById("alert").innerText = "Failed to update your profile !!";
            document.getElementById("alert").classList.add('alert-danger');
            document.getElementById("alert").removeAttribute("style");
        });
    }
}

function addNewInfo(userViewEntity) {
    document.getElementById("firstname").innerHTML = userViewEntity.FirstName;
    document.getElementById("lastneme").innerHTML = userViewEntity.LastName;
    document.getElementById("username").innerHTML = userViewEntity.UserName;
    document.getElementById("email").innerHTML = userViewEntity.Email;
    document.getElementById("phone").innerHTML = userViewEntity.Phone;

}


function validateForm()
{
    if (firstnameInput.classList.contains('is-invalid') ||
        lastnameInput.classList.contains('is-invalid')  ||
        passwordInput.classList.contains('is-invalid')  ||
        phoneInput.classList.contains('is-invalid')) {
        console.log(false)
        return false;
    }else {
        console.log(true)
        return true;
    }
}
