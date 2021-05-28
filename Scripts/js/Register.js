
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
    dropdown = document.getElementById("Actor_UserTypeId");
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
    phone.addEventListener('input', validatePhone)
    password.addEventListener('input', validatePassword)
    firstname.addEventListener('input', validateFirstname);
    lastname.addEventListener('input', validateLastname);
}

function validateFirstname() {
    firstname.classList.remove('is-invalid')
    firstname.classList.remove('is-valid')
    if (firstname.value != null && firstname.value.length > 2) 
        firstname.classList.add('is-valid')
    else
        firstname.classList.add('is-invalid')
}

function validateLastname() {
    lastname.classList.remove('is-invalid')
    lastname.classList.remove('is-valid')
    if (lastname.value != null && lastname.value.length > 2)
        lastname.classList.add('is-valid')
    else
        lastname.classList.add('is-invalid')
}

function validatePassword() {
    var passReg = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
    password.classList.remove('is-invalid')
    password.classList.remove('is-valid')
    if (passReg.test(password.value))
        password.classList.add('is-valid')
    else
        password.classList.add('is-invalid')
}


function validatePhone() {
    phone.classList.remove('is-invalid')
    phone.classList.remove('is-valid')
    if (phone.value.length === 11)
        phone.classList.add('is-valid')
    else
        phone.classList.add('is-invalid')
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

    if (email.value != null && email.value.length > 2)
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