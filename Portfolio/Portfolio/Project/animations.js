// This script will handle the scroll-in animations for sections.

document.addEventListener("DOMContentLoaded", function () {

    // Options for the Intersection Observer
    // The animation will trigger when 5% of the element is visible.
    const options = {
        threshold: 0.05
    };

    // The function that runs when an element comes into view
    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            // If the element is on screen
            if (entry.isIntersecting) {
                // Add the 'visible' class to trigger the animation
                entry.target.classList.add('visible');
                // Stop watching this element so the animation only happens once
                observer.unobserve(entry.target);
            }
        });
    }, options);

    // Find all the sections we want to animate
    const sectionsToAnimate = document.querySelectorAll('.fade-in-section');

    // Tell the observer to watch each of them
    sectionsToAnimate.forEach(section => {
        observer.observe(section);
    });

});
