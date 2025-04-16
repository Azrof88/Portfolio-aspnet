<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Portfolio.Project.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>H.M. Azrof | Portfolio</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" href="sliderMenu.css" />
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




    <script src="script.js"></script>
    </body>
</html>
