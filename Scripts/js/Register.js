
var dropdown;
var username;
var email;
var firstname;
var lastname;
var phone;
var submitButton
var password;

function initialize()
{
    username = document.getElementById('usernameInput');
    email = document.getElementById('emailInput');
    dropdown = document.getElementById("UserTypeId");
    firstname = document.getElementById("firstnameInput");
    lastname = document.getElementById("lastnameInput");
    phone = document.getElementById('phoneInput');
    submitButton = document.getElementById('submitButton');
    password = document.getElementById('passwordInput');
    addEventListener();
}

function addEventListener() {
    username.addEventListener('change', checkUsername);
    dropdown.addEventListener('change', checkDrobDown);
    email.addEventListener('change', checkEmail);
    phone.addEventListener('input', function () { validatePhone(this) })
    password.addEventListener('input', function () { validatePassword(this) })
    firstname.addEventListener('input', function () { validateFirstname(this) });
    lastname.addEventListener('input', function () { validateLastname(this) });
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

function checkDrobDown() {
    username.classList.remove('is-invalid')
    username.classList.remove('is-valid')
    username.value = null
    if (this.options[this.selectedIndex].text == "editor") {
        username.removeAttribute("style");
    } else {
        username.setAttribute("style", "display:none")
        username.classList.add('is-valid')
    }
}

function checkEmail() {
    email.classList.remove('is-invalid')
    email.classList.remove('is-valid')
    var reg = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (email.value != null && reg.test(email.value))
        $.ajax({
            type: "POST",
            url: "/Actor/CheckEmail",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ email : email.value }),
            success: function (response) {
                if (!response.result)
                    email.classList.add('is-valid');
                else
                    email.classList.add('is-invalid')
            }
        });
    else
        email.classList.add('is-invalid')

}

function checkUsername() {
    username.classList.remove('is-invalid')
    username.classList.remove('is-valid')

    if (username.value != null && username.value.length > 2) {
        $.ajax({
            type: "POST",
            url: "/Actor/CheckUserName",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ username: username.value }),
            success: function (response) {
                if (!response.result)
                    username.classList.add('is-valid');
                else
                    username.classList.add('is-invalid')
            }
        });
    }
    else
        username.classList.add('is-invalid')
  
}

function validateForm() {
    if (firstname.classList.contains('is-valid') &&
        lastname.classList.contains('is-valid') &&
        email.classList.contains('is-valid') &&
        password.classList.contains('is-valid') &&
        phone.classList.contains('is-valid') &&
        username.classList.contains('is-valid')) {
        console.log(true)
        return true;
    }
    else {
        console.log(false)
        return false;
    }
}