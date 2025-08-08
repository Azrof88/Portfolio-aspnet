// File: slidermenu.js

// --- Menu Toggle Function (Your corrected version is good) ---
function toggleMenu(event) {
    event.preventDefault(); // Prevents the page from reloading

    try {
        const menu = document.getElementById("sliderMenu");
        menu.classList.toggle("active");
    } catch (error) {
        console.error("Error in toggleMenu:", error);
    }
}


// --- Active Class on Click (This logic is fine) ---
document.querySelectorAll('.menu-items a').forEach(link => {
    link.addEventListener('click', function (e) {
        // Close the menu when a link is clicked (good for mobile UX)
        document.getElementById("sliderMenu").classList.remove("active");

        // Update the active class
        document.querySelectorAll('.menu-items a').forEach(nav =>
            nav.classList.remove('active'));
        this.classList.add('active');
    });
});


// --- Scrollspy Logic (PERFORMANCE OPTIMIZED) ---

// Helper function to limit how often a function can run.
// This prevents the scroll event from firing too frequently.
function throttle(func, limit) {
    let inThrottle;
    return function () {
        const args = arguments;
        const context = this;
        if (!inThrottle) {
            func.apply(context, args);
            inThrottle = true;
            setTimeout(() => inThrottle = false, limit);
        }
    }
}

// Select the elements once, outside of the scroll handler, for better performance.
const sections = document.querySelectorAll('section');
const navLinks = document.querySelectorAll('.menu-items a');

// The function that will check the scroll position.
function handleScrollSpy() {
    sections.forEach(section => {
        const sectionTop = section.offsetTop;
        const sectionHeight = section.clientHeight;
        // Add a small offset to make the highlight trigger at a better position.
        const scrollPosition = window.scrollY + (window.innerHeight / 2);

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
}

// Attach the throttled function to the scroll event.
// This will run the handleScrollSpy function at most once every 150 milliseconds.
window.addEventListener('scroll', throttle(handleScrollSpy, 150));
