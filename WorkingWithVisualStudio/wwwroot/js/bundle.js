document.addEventListener("DOMContentLoaded", function () {
    var element = document.createElement("p");
    element.textContent = "This is the element from the (m) third.js file";
    document.querySelector("body").appendChild(element);
})
document.addEventListener("DOMContentLoaded", function () {
    var element = document.createElement("p");
    element.textContent = "This is the element from the (m) fourth.js file";
    document.querySelector("body").appendChild(element);
})