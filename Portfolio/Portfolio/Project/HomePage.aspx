<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" CodeFile="~/Project/HomePage.aspx.cs" Inherits="Portfolio.Project.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>H.M. Azrof | Portfolio</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="sliderMenu.css" />
    <link rel="stylesheet" type="text/css" href="mainContent.css" />
    <link rel="stylesheet" type="text/css" href="skills.css" />
    <link rel="stylesheet" type="text/css" href="project.css" />
    <link rel="stylesheet" type="text/css" href="contacts.css" />
</head>
<body style="background:linear-gradient(135deg, #0f0c29, #302b63);">
    <!-- Toggle Button -->
    <button class="menu-toggle" onclick="toggleMenu()">&#x2630;</button>

    <!-- Slider Menu Container -->
    <div class="slider-menu" id="sliderMenu">
        <!-- Profile Section -->
        <div class="menu-header">
            <img src="Azrof.jpg" alt="User Profile" class="profile-image"/>
            <h2 class="profile-name">H.M. Azrof</h2>

        </div>
        <!-- Menu Items -->
        <div class="menu-items">
            <a href="#form1">Home</a>
            <a href="#skillsSection">Skills</a>
            <a href="#projectsCarousal">Project</a>
            <a href="#contact">Contacts</a>
        </div>
        <!-- Close Button-->
        <button class="close-btn" onclick="toggleMenu()">×</button>
    </div>

    <section id="form1" runat="server">
        <div class="hero-container">
            <!-- Image Slider -->
            <div class="slider-container" id="sliderContainer" runat="server">
                <!-- Slides will be dynamically injected here -->
            </div>
            
            <!-- Content Overlay -->
            <div class="content-overlay">
                <div class="content">
                    <h1>Hi, I'm <span class="highlight">H.M. Azrof</span></h1>
                    <h2>Aspiring Full-Stack Developer</h2>
                    <p>I love building modern web applications using HTML, CSS, JavaScript & ASP.NET.</p>
                    <div class="mainContentButtons">
                        <a href="#contact" class="btn">Contacts Me</a>
                    </div>
                </div>
            </div>

            <!-- Slider Controls -->
            <div class="slider-controls">
                <button class="prev-btn">❮</button>
                <div class="dots-container" id="dotsContainer" runat="server"></div>
                <button class="next-btn">❯</button>
            </div>
        </div>
    </section>


    <section class="skills-section" id="skillsSection">
    <h2 class="section-title">Technical Expertise</h2>
    <div class="skills-grid">
        <!-- C# /.NET Card -->
        <a href="https://github.com/Azrof88/programming-portfolio" class="skill-card" target="_blank" rel="noopener" aria-label="C# Projects">
            <div class="card-icon">
                <i class="fas fa-code"></i>
            </div>
            <h3>C# / .NET 8</h3>
            <p>Enterprise-level application development</p>
        </a>

        <!-- React Card -->
        <a href="https://github.com/Azrof88/programming-portfolio" class="skill-card" target="_blank" rel="noopener" aria-label="React Projects">
            <div class="card-icon">
                <i class="fab fa-react"></i>
            </div>
            <h3>React 21</h3>
            <p>Modern UI/UX implementations</p>
        </a>

        <!-- SQL Server Card -->
        <a href="https://github.com/Azrof88/programming-portfolio" class="skill-card" target="_blank" rel="noopener" aria-label="SQL Projects">
            <div class="card-icon">
                <i class="fas fa-database"></i>
            </div>
            <h3>SQL Server</h3>
            <p>Database design & optimization</p>
        </a>

        <!-- Azure DevOps Card -->
        <a href="https://github.com/Azrof88/programming-portfolio" class="skill-card" target="_blank" rel="noopener" aria-label="Azure Projects">
            <div class="card-icon">
                <i class="fas fa-cloud"></i>
            </div>
            <h3>Azure DevOps</h3>
            <p>Cloud solutions & CI/CD pipelines</p>
        </a>

        <!-- Android/Desktop Card -->
        <a href="https://github.com/Azrof88/programming-portfolio" class="skill-card" target="_blank" rel="noopener" aria-label="Android Projects">
            <div class="card-icon">
                <i class="fas fa-mobile-alt"></i>
            </div>
            <h3>Android/Desktop</h3>
            <p>Cross-platform applications</p>
        </a>

        <!-- C/C++/Java Card -->
        <a href="https://github.com/Azrof88/programming-portfolio" class="skill-card" target="_blank" rel="noopener" aria-label="C++ Projects">
            <div class="card-icon">
                <i class="fas fa-file-code"></i>
            </div>
            <h3>C/C++/Java</h3>
            <p>Core programming expertise</p>
        </a>
    </div>
</section>

    <!-- Projects Section -->
<section class="projects-carousel" id="projectsCarousal">
    <div class="filter-controls">
        <button class="filter-btn active" data-filter="all">All</button>
        <button class="filter-btn" data-filter="android">Android</button>
        <button class="filter-btn" data-filter="web">Web</button>
        <button class="filter-btn" data-filter="database">Database</button>
    </div>

    <div class="carousel-container">
        <div class="carousel-track" id="carouselTrack">
            <!-- Project cards will be injected here -->
        </div>
    </div>

    <div class="carousel-nav">
        <button class="nav-btn prev-btn">&lt;</button>
        <button class="nav-btn next-btn">&gt;</button>
    </div>
</section>

    <!-- Contact Section -->
    <form id="contactForm" runat="server">
        <section id="contact" class="section contact">
            <div class="section-header">
                <h2 class="section-title">Let's Collaborate</h2>
                <p class="section-subtitle">Response within 24 business hours</p>
            </div>

            <div class="contact-container">
                <div class="contact-form">
                    <div class="form-group">
                        <asp:TextBox ID="Name" runat="server" CssClass="floating-input" placeholder=" " required="true" />
                        <label class="floating-label"><i class="fas fa-user-tag"></i> Full Name</label>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="Email" runat="server" CssClass="floating-input" TextMode="Email" placeholder=" " required="true" />
                        <label class="floating-label"><i class="fas fa-at"></i> Professional Email</label>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="Phone" runat="server" CssClass="floating-input" TextMode="Phone" placeholder=" " />
                        <label class="floating-label"><i class="fas fa-mobile-alt"></i> Contact Number (Optional)</label>
                    </div>

                    <div class="form-group">
                        <asp:DropDownList ID="Subject" runat="server" CssClass="floating-select" required="true">
                            <asp:ListItem Text="" Value="" />
                            <asp:ListItem Text="Technical Consulting" Value="consulting" />
                            <asp:ListItem Text="Project Collaboration" Value="collaboration" />
                            <asp:ListItem Text="Career Opportunity" Value="opportunity" />
                            <asp:ListItem Text="Other Inquiry" Value="other" />
                        </asp:DropDownList>
                        <label class="floating-label"><i class="fas fa-comment-dots"></i> Inquiry Type</label>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="Message" runat="server" CssClass="floating-textarea" TextMode="MultiLine" Rows="5" placeholder=" " required="true" />
                        <label class="floating-label"><i class="fas fa-envelope-open-text"></i> Detailed Message</label>
                    </div>

                    <asp:TextBox ID="Website" runat="server" CssClass="hp-field" placeholder="Leave this blank" />

                    <div class="form-actions">
                        <asp:Button ID="SubmitBtn" runat="server" Text="Send Message" CssClass="submit-btn" OnClick="SubmitBtn_Click" />
                        <asp:Label ID="StatusLabel" runat="server" CssClass="form-status" />
                    </div>
                </div>
            </div>
        </section>
    </form>









    <script src="sliderMenu.js"></script>
    <script src="mainContent.js"></script>
    
    <script src="project.js"></script>
    </body>
</html>
