function toggleMenu() {

    try {
        const menu = document.getElementById("sliderMenu");
        menu.classList.toggle("active");
    } catch (Exception) {
        alert("error")

    }

}
// Add active class on click
document.querySelectorAll('.menu-items a').forEach(link => {
    link.addEventListener('click', function (e) {
        // Remove active class from all links
        document.querySelectorAll('.menu-items a').forEach(nav =>
            nav.classList.remove('active'));

        // Add active class to clicked link
        this.classList.add('active');
    });
});

// Update active class based on scroll position
window.addEventListener('scroll', () => {
    const sections = document.querySelectorAll('section');
    const navLinks = document.querySelectorAll('.menu-items a');

    sections.forEach(section => {
        const sectionTop = section.offsetTop;
        const sectionHeight = section.clientHeight;
        const scrollPosition = window.scrollY + 100;

        if (scrollPosition >= sectionTop &&
            scrollPosition < sectionTop + sectionHeight) {
            const id = section.getAttribute('id');
            navLinks.forEach(link => {
                link.classList.remove('active');
                if (link.getAttribute('href') === `#${id}`) {
                    link.classList.add('active');
                }
            });
        }
    });
});



