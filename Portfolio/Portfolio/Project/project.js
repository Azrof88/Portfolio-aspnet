// This script now only handles the CLIENT-SIDE interactivity (filtering and navigation).

function initializeCarousel() {
    const track = document.getElementById('carouselTrack');
    const carouselContainer = track.parentElement; // The direct parent with the scrollbar
    if (!track || !carouselContainer) return; // Safety check

    // --- DYNAMIC SCROLL AMOUNT CALCULATION ---
    let scrollAmount = 340; // Default value
    const firstCard = track.querySelector('.project-card');
    if (firstCard) {
        const cardStyle = window.getComputedStyle(firstCard);
        const cardMargin = parseFloat(cardStyle.marginRight) || 0;
        scrollAmount = firstCard.offsetWidth + cardMargin;
    }

    // --- FILTERING LOGIC ---
    document.querySelectorAll('.filter-btn').forEach(btn => {
        btn.addEventListener('click', (event) => {
            event.preventDefault();
            document.querySelector('.filter-btn.active')?.classList.remove('active');
            btn.classList.add('active');
            const filter = btn.dataset.filter;

            track.querySelectorAll('.project-card').forEach(card => {
                if (filter === 'all' || card.dataset.category === filter) {
                    card.classList.remove('hidden');
                } else {
                    card.classList.add('hidden');
                }
            });
            // Reset scroll position after filtering
            carouselContainer.scrollLeft = 0;
        });
    });

    // --- IMPROVED CAROUSEL NAVIGATION LOGIC ---
    const projectCarouselContainer = document.querySelector('.projects-carousel');
    const nextButton = projectCarouselContainer.querySelector('.next-btn');
    const prevButton = projectCarouselContainer.querySelector('.prev-btn');

    nextButton.addEventListener('click', (event) => {
        event.preventDefault();
        // Calculate the maximum scroll position
        const maxScrollLeft = carouselContainer.scrollWidth - carouselContainer.clientWidth;

        // Check if we are not at the end before scrolling
        if (carouselContainer.scrollLeft < maxScrollLeft) {
            carouselContainer.scrollBy({ left: scrollAmount, behavior: 'smooth' });
        }
    });

    prevButton.addEventListener('click', (event) => {
        event.preventDefault();
        // Check if we are not at the beginning before scrolling
        if (carouselContainer.scrollLeft > 0) {
            carouselContainer.scrollBy({ left: -scrollAmount, behavior: 'smooth' });
        }
    });

    // --- NEW: DRAG-TO-SCROLL LOGIC ---
    let isDown = false;
    let startX;
    let scrollLeft;

    carouselContainer.addEventListener('mousedown', (e) => {
        isDown = true;
        carouselContainer.classList.add('active');
        startX = e.pageX - carouselContainer.offsetLeft;
        scrollLeft = carouselContainer.scrollLeft;
    });

    carouselContainer.addEventListener('mouseleave', () => {
        isDown = false;
        carouselContainer.classList.remove('active');
    });

    carouselContainer.addEventListener('mouseup', () => {
        isDown = false;
        carouselContainer.classList.remove('active');
    });

    carouselContainer.addEventListener('mousemove', (e) => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - carouselContainer.offsetLeft;
        const walk = (x - startX) * 2; // The multiplier changes the scroll speed
        carouselContainer.scrollLeft = scrollLeft - walk;
    });
}

// Initialize all the interactive parts once the HTML document is fully loaded.
document.addEventListener('DOMContentLoaded', initializeCarousel);
