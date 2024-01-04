function openNavbar() {
    var x = document.getElementById("sidebar");
    if (x.className === "sidebar sidebar-offcanvas") {
        x.className += " active";
    } else {
        x.className = "sidebar sidebar-offcanvas";
    }
}
