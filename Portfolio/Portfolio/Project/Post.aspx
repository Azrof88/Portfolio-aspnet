<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="Portfolio.Project.Post" MasterPageFile="~/Project/Portfolio.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="post" class="py-5">
        <div class="container">
            <div class="row">
                <div class="col-md-8 offset-md-2">
                    <!-- The full blog post content will be loaded here -->
                    <asp:Literal ID="litPostContent" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

