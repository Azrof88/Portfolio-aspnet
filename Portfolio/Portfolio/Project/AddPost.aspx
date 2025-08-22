<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPost.aspx.cs" Inherits="Portfolio.Project.AddPost" MasterPageFile="~/Project/Admin.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Add New Blog Post</h2>
        </div>
        <div class="card-body">
            <div class="mb-3">
                <asp:Label runat="server" Text="Post Title" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Excerpt (A short summary)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtExcerpt" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Full Content (you can use HTML tags like <p> and <strong>)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
            </div>

            <asp:Button ID="btnSave" runat="server" Text="Publish Post" OnClick="btnSave_Click" CssClass="btn btn-primary" />
            <asp:HyperLink runat="server" NavigateUrl="~/Project/Admin.aspx" Text="Cancel" CssClass="btn-cancel" />
            <br />
            <asp:Label ID="lblStatus" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
    </div>
</asp:Content>
