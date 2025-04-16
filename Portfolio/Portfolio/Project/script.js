function toggleMenu() {

    try {
        const menu = document.getElementById("sliderMenu");
        menu.classList.toggle("active");
    } catch (Exception) {
        alert("error")

    }

}

class ImageSlider {
    constructor() {
        this.slides = document.querySelectorAll('.slide');
        this.dots = document.querySelectorAll('.dot');
        this.currentIndex = 0;
        this.interval = null;
        this.init();
    }

    init() {
        // Initialize first slide
        this.showSlide(this.currentIndex);

        // Auto-advance every 5 seconds
        this.startAutoSlide();

        // Event listeners
        document.querySelector('.prev-btn').addEventListener('click', () => this.prevSlide());
        document.querySelector('.next-btn').addEventListener('click', () => this.nextSlide());

        this.dots.forEach((dot, index) => {
            dot.addEventListener('click', () => this.goToSlide(index));
        });

        // Pause on hover
        document.querySelector('.slider-container').addEventListener('mouseenter', () =>
            this.stopAutoSlide());
        document.querySelector('.slider-container').addEventListener('mouseleave', () =>
            this.startAutoSlide());
    }

    showSlide(index) {
        this.slides.forEach(slide => slide.classList.remove('active'));
        this.dots.forEach(dot => dot.classList.remove('active'));

        this.slides[index].classList.add('active');
        this.dots[index].classList.add('active');
    }

    nextSlide() {
        this.currentIndex = (this.currentIndex + 1) % this.slides.length;
        this.showSlide(this.currentIndex);
    }

    prevSlide() {
        this.currentIndex = (this.currentIndex - 1 + this.slides.length) % this.slides.length;
        this.showSlide(this.currentIndex);
    }

    goToSlide(index) {
        this.currentIndex = index;
        this.showSlide(index);
    }

    startAutoSlide() {
        this.interval = setInterval(() => this.nextSlide(), 5000);
    }

    stopAutoSlide() {
        clearInterval(this.interval);
    }
}

// Initialize slider when DOM loads
document.addEventListener('DOMContentLoaded', () => new ImageSlider());
