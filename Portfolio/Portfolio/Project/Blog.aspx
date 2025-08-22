<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="Portfolio.Project.Blog" MasterPageFile="~/Project/Portfolio.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="blog" class="py-5">
        <div class="container">
            <h2 class="section-title">My Blog</h2>
            <p class="section-subtitle">Thoughts on technology, development, and my journey.</p>
            
            <div class="row">
                <!-- Blog posts will be dynamically loaded here -->
                <asp:Literal ID="litBlogPosts" runat="server"></asp:Literal>
            </div>
        </div>
    </section>
</asp:Content>

