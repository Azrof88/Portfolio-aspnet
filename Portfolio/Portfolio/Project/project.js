// Project Data Structure
const projects = [
    {
        title: "Android Fitness Tracker",
        category: "android",
        image: "fitness-app.jpg",
        description: "Health monitoring app with sensor integration",
        github: "https://github.com/Azrof88/programming-portfolio",
        tech: ["Kotlin", "Firebase"]
    },
    {
        title: "E-Commerce Dashboard",
        category: "web",
        image: "dashboard.jpg",
        description: "Real-time analytics platform",
        github: "https://github.com/Azrof88/programming-portfolio",
        tech: ["React", "Node.js"]
    },
    // Add more projects...
    {
        title: "E-Commerce Dashboard",
        category: "web",
        image: "dashboard.jpg",
        description: "Real-time analytics platform",
        github: "https://github.com/Azrof88/programming-portfolio",
        tech: ["React", "Node.js"]
    },
    {
        title: "E-Commerce Dashboard",
        category: "web",
        image: "dashboard.jpg",
        description: "Real-time analytics platform",
        github: "https://github.com/Azrof88/programming-portfolio",
        tech: ["React", "Node.js"]
    },
    {
        title: "E-Commerce Dashboard",
        category: "web",
        image: "dashboard.jpg",
        description: "Real-time analytics platform",
        github: "https://github.com/Azrof88/programming-portfolio",
        tech: ["React", "Node.js"]
    },
    {
        title: "E-Commerce Dashboard",
        category: "web",
        image: "dashboard.jpg",
        description: "Real-time analytics platform",
        github: "https://github.com/Azrof88/programming-portfolio",
        tech: ["React", "Node.js"]
    }
];
let currentPosition = 0;
const track = document.getElementById('carouselTrack');

function renderProjects(filter = 'all') {
    track.innerHTML = projects
        .filter(project => filter === 'all' || project.category === filter)
        .map(project => `
            <div class="project-card" data-category="${project.category}">
                <img src="${project.image}" alt="${project.title}" class="project-image">
                <h3>${project.title}</h3>
                <p>${project.description}</p>
                <div class="tech-stack">
                    ${project.tech.map(t => `<span>${t}</span>`).join('')}
                </div>
                <a href="${project.github}" class="github-link" target="_blank">
                    <i class="fab fa-github"></i>
                </a>
            </div>
        `).join('');
}

document.querySelectorAll('.filter-btn').forEach(btn => {
    btn.addEventListener('click', () => {
        document.querySelector('.filter-btn.active')?.classList.remove('active');
        btn.classList.add('active');
        renderProjects(btn.dataset.filter);
        resetCarouselPosition();
    });
});

document.querySelector('.next-btn').addEventListener('click', () => {
    currentPosition += window.innerWidth * 0.8;
    smoothScrollTo(currentPosition);
});

document.querySelector('.prev-btn').addEventListener('click', () => {
    currentPosition -= window.innerWidth * 0.8;
    smoothScrollTo(currentPosition);
});

function smoothScrollTo(position) {
    track.style.transform = `translateX(-${position}px)`;
}

function resetCarouselPosition() {
    currentPosition = 0;
    smoothScrollTo(0);
}

// Initialize
renderProjects();