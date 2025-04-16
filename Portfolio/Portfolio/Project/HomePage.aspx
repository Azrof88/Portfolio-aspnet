<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Portfolio.Project.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>H.M. Azrof | Portfolio</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="sliderMenu.css" />
    <link rel="stylesheet" type="text/css" href="mainContent.css" />
</head>
<body>
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
            <a href="#home">Home</a>
            <a href="#about">About</a>
            <a href="#project">Project</a>
            <a href="#contacts">Contacts</a>
        </div>
        <!-- Close Button-->
        <button class="close-btn" onclick="toggleMenu()">×</button>
    </div>

    <form id="form1" runat="server">
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
                        <a href="#contact" class="btn">Hire Me</a>
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
    </form>



    <script src="script.js"></script>
    </body>
</html>
