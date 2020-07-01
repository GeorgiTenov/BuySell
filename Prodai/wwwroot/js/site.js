// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var name = document.getElementById("name");

function Try(name) {
    if (name.length <= 0) {
        alert("Please fields are required");
    }
}

function sayHello(name) {
    alert("Hello");
    name.value = "hello";
}
name.addEventListener("click",sayHello(name));