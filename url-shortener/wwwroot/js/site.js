// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const submitBtn = document.getElementById("submit");
const urlInput = document.getElementById("urlshort");
const responseArea = document.getElementById("resp-area");

submitBtn.addEventListener("click", (ev) => {
    let url = urlInput.value;
    fetch("/", {
        method: "POST",
        body: JSON.stringify(url),
        headers: {
            "Content-Type": "application/json"
        }
    }).then(res => res.json())
        .then(response => {
            console.log(response);
            responseArea.innerHTML = "<a href='https://spataco.li/" + response.token +"'>https://spataco.li/" + response.token + "</a>";
        });
});
