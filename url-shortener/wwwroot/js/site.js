// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const submitBtn = document.getElementById("submit");
const urlInput = document.getElementById("urlshort");
const responseArea = document.getElementById("resp-area");

submitBtn.addEventListener("click", (ev) => {
    let url = urlInput.value;
    if (url === "") {
        return;
    }
    fetch("/", {
        method: "POST",
        body: JSON.stringify(url),
        headers: {
            "Content-Type": "application/json"
        }
    }).then(res => res.json())
        .then(response => {
            console.log(response);
            responseArea.innerHTML = "<a href='https://sptc.li/" + response.token +"'>https://sptc.li/" + response.token + "</a>";
        });
});

const urls = document.getElementsByClassName("delete-url");
[...urls].forEach(url => url.addEventListener("click", (ev) => {
    const srcElement = ev.srcElement;
    const token = srcElement.getAttribute("data-token");
    fetch("/" + token, {
        method: "DELETE",
        body: JSON.stringify(token),
        headers: {
            "Content-Type": "application/json"
        }
    }).then(res => res.json())
        .then(response => {
            console.log(response);
            responseArea.innerHTML = response.status;
        })
    alert(token);
}))
