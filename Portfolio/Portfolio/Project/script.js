function toggleMenu() {

    try {
        const menu = document.getElementById("sliderMenu");
        menu.classList.toggle("active");
    } catch (Exception) {
        alert("error")

    }

}