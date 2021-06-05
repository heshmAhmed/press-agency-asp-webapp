hasinteraction = true
function setInteractions(interactions) {
    if (interactions == null)
        return ;
    for (i = 0; i < interactions.length; i++) {
        postId = interactions[i].PostID;
        IsLike = interactions[i].IsLike;
        if (IsLike) {
            document.getElementById("btn_like_" + postId).classList.remove("btn-light")
            document.getElementById("btn_like_" + postId).classList.add("btn-primary")
        }
        else {
            document.getElementById("btn_dislike_" + postId).classList.remove("btn-light")

            document.getElementById("btn_dislike_" + postId).classList.add("btn-primary")
        }
    }
    
}

function setSavedPosts(savedPostsIds) {
    if (savedPostsIds == null)
        return;
    for (i = 0; i < savedPostsIds.length; i++) {
        var btn = document.getElementById("btn_save_" + savedPostsIds[i]);
        console.log(savedPostsIds[i])
        btn.innerHTML == "saved";
        btn.classList.add("disabled");
    }
}



function like(postId, defualtnodislikes, loggedIn) {
    if (!loggedIn)
        return;
    btn_like = document.getElementById("btn_like_" + postId);
    btn_dislike = document.getElementById("btn_dislike_" + postId);
    nolikes = document.getElementById("no_like_" + postId);
    nodislikes = document.getElementById("no_dislike_" + postId);

    if (btn_like.classList.contains("btn-primary")) {
        removeInteraction(postId, btn_like, nolikes);
        return;
    }

    sendLikeRe(postId);
    handleBtnColor(btn_like, btn_dislike);
    decreaseNoInteracts(nodislikes, defualtnodislikes);
    nolikes.innerText++;
}

function disLike(postId, defualtnolikes, loggedIn) {
    if (!loggedIn)
        return

    btn_like = document.getElementById("btn_like_" + postId);
    btn_dislike = document.getElementById("btn_dislike_" + postId);
    nolikes = document.getElementById("no_like_" + postId);
    nodislikes = document.getElementById("no_dislike_" + postId);

    if (btn_dislike.classList.contains("btn-primary")) {
        removeInteraction(postId, btn_dislike, nodislikes);
        return;
    }

    sendDisLikeRe(postId);
    handleBtnColor(btn_dislike, btn_like);
    decreaseNoInteracts(nolikes, defualtnolikes);
    nodislikes.innerText++;
}

function handleBtnColor(btn1, btn2) {
    btn1.classList.remove("btn-light");
    btn2.classList.remove("btn-primary");
    btn1.classList.add("btn-primary");
    btn2.classList.add("btn-light");
 }

function removeInteraction(postId, btn, number) {
    btn.classList.remove("btn-primary");
    btn.classList.add("btn-light");
    number.innerText = number.innerText == 0 ? 0 : number.innerText - 1;
    $.ajax({
        type: "GET",
        data: {
            postId: postId
        },
        url: "/Viewer/RemoveInteraction",
        dataType: "json",
        success: function (response) {
            console.log(response);
        }
    }).fail(function (fail) {
        console.log(false)
    });
}

function sendLikeRe(postId) {
    $.ajax({
        type: "GET",
        data: {
            postId: postId
        },
        url: "/Viewer/Like",
        dataType: "json",
        success: function (response) {
            console.log(response)
        }
    }).fail(function (fail) {
        console.log(false)
    });
}

function sendDisLikeRe(postId) {
    $.ajax({
        type: "GET",
        data: {
            postId: postId
        },
        url: "/Viewer/DisLike",
        dataType: "json",
        success: function (response) {
            console.log(response)
        }
    }).fail(function (fail) {
        console.log(false)
    });
}

function decreaseNoInteracts(componenet, defualtno) {
    if (hasinteraction)
        componenet.innerText = componenet.innerText == 0 ? 0 : componenet.innerText - 1;
    else
        componenet.innerText = defualtno
}


function submitForm() {
    validationAlert = document.getElementById("validationAlert");
    validationAlert.setAttribute("style", "display:none");
    $.ajax({
        type: "POST",
        url: "/Actor/PopUpLogIn",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            Identity: $('#identity').val(),
            Password: $('#password').val(),
        }),
        success: function (response) {
            if (response.result)
                window.location.href = "";
            else
                validationAlert.removeAttribute("style");
        }
    }).fail(function () {
        validationAlert.removeAttribute("style");
    });
}

function savePost(postId, loggedIn, isViewer) {
    var btn = document.getElementById("btn_save_" + postId);
    if (!loggedIn || !isViewer || btn.classList.contains("disabled"))
        return;
    btn.innerHTML = "Saved";
    btn.classList.add("disabled");
    $.ajax({
        type: "Get",
        url: "/Viewer/SavePost",
        dataType: "json",
        data: {
            postId: postId
        },
        success: function (response) {
            console.log(response)
        }
    });
}