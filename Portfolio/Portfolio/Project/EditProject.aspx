<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="Portfolio.Project.AddProject" MasterPageFile="~/Project/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <%-- The title will change depending on the page --%>
            <h2><%= Page.Title %></h2> 
        </div>
        <div class="card-body">
            <div class="mb-3">
                <asp:Label runat="server" Text="Title" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Category (e.g., web, android, database)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Image URL (e.g., my-image.jpg)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtImageURL" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Description" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="GitHub URL" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtGitHubURL" runat="server" CssClass="form-control" TextMode="Url"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Tech Stack (comma-separated, e.g., C#,SQL,JavaScript)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTechStack" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <%-- This button will be for "Save" on the Add page and "Update" on the Edit page --%>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
            <asp:HyperLink ID="lnkCancel" runat="server" NavigateUrl="~/Project/Admin.aspx" Text="Cancel" CssClass="btn-cancel" />
            <br />
            <asp:Label ID="lblStatus" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
    </div>
</asp:Content>
