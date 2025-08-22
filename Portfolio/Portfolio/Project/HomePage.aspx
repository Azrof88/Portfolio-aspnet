<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Portfolio.Project.HomePage" MasterPageFile="~/Project/Portfolio.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ======================= HERO SECTION ======================= -->
    <section id="home" class="hero">
        <div class="container">
            <div class="row align-items-center g-5">
                <div class="col-lg-6">
                    <h1 class="display-4">Hi, I'm <span class="text-primary">H.M. Azrof</span></h1>
                    <p class="lead my-4">An aspiring Full-Stack Developer specializing in building robust and scalable web applications with ASP.NET and modern frontend technologies.</p>
                    <a href="#projects" class="btn btn-primary btn-lg">View My Work</a>
                </div>
                <div class="col-lg-6 text-center">
                    <img src="Azrof.jpg" alt="H.M. Azrof" class="img-fluid hero-img rounded-circle" />
                </div>
            </div>
        </div>
    </section>

    <!-- ======================= SKILLS SECTION ======================= -->
    <section id="skills">
        <div class="container">
            <h2 class="section-title">Technical Expertise</h2>
            <p class="section-subtitle">My proficiency in various technologies</p>
            <div class="row g-4">
                <!-- The C# code will populate this literal control with skill cards -->
                <asp:Literal ID="litSkills" runat="server"></asp:Literal>
            </div>
        </div>
    </section>

    <!-- ======================= PROJECTS SECTION ======================= -->
    <section id="projects" class="bg-light bg-opacity-10">
        <div class="container">
            <h2 class="section-title">Project Showcase</h2>
            <p class="section-subtitle">A selection of my recent work</p>
            <div class="row g-4">
                <!-- C# code will populate this literal control with project cards -->
                <asp:Literal ID="litProjects" runat="server"></asp:Literal>
            </div>
        </div>
    </section>

    <!-- ======================= CONTACT & GUESTBOOK ======================= -->
    <section id="contact">
        <div class="container">
            <div class="row g-5">
                <!-- CONTACT FORM -->
                <div class="col-lg-6">
                    <h2 class="section-title text-start">Let's Collaborate</h2>
                    <div class="form-group mb-3">
                        <asp:Label runat="server" Text="Full Name" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="Name" runat="server" CssClass="form-control" ValidationGroup="Contact"></asp:TextBox>
                    </div>
                    <div class="form-group mb-3">
                        <asp:Label runat="server" Text="Professional Email" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="Email" runat="server" CssClass="form-control" TextMode="Email" ValidationGroup="Contact"></asp:TextBox>
                    </div>
                    <div class="form-group mb-3">
                        <asp:Label runat="server" Text="Message" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="Message" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" ValidationGroup="Contact"></asp:TextBox>
                    </div>
                    <asp:Button ID="SubmitBtn" runat="server" Text="Send Message" CssClass="btn btn-primary" OnClick="SubmitBtn_Click" ValidationGroup="Contact" />
                    <asp:Label ID="StatusLabel" runat="server" CssClass="d-block mt-3"></asp:Label>
                </div>
                <!-- GUESTBOOK -->
                <div class="col-lg-6">
                    <h2 class="section-title text-start">Guestbook</h2>
                    <div class="form-group mb-3">
                        <asp:TextBox ID="txtGuestName" runat="server" placeholder="Your Name" CssClass="form-control" ValidationGroup="Guestbook"></asp:TextBox>
                    </div>
                    <div class="form-group mb-3">
                        <asp:TextBox ID="txtGuestMessage" runat="server" placeholder="Your message..." CssClass="form-control" TextMode="MultiLine" Rows="3" ValidationGroup="Guestbook"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnSubmitComment" runat="server" Text="Sign Guestbook" OnClick="btnSubmitComment_Click" CssClass="btn btn-secondary" ValidationGroup="Guestbook" />
                    <asp:Label ID="lblCommentStatus" runat="server" CssClass="d-block mt-3"></asp:Label>
                    <hr class="my-4" />
                    <div class="comments-display" style="height: 200px; overflow-y: auto;">
                        <asp:Literal ID="litComments" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
