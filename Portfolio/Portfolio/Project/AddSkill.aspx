<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSkill.aspx.cs" Inherits="Portfolio.Project.AddSkill" MasterPageFile="~/Project/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Add New Skill</h2>
        </div>
        <div class="card-body">
            <div class="mb-3">
                <asp:Label runat="server" Text="Skill Name (e.g., C# / .NET 8)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtSkillName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Description" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Font Awesome Icon Class (e.g., fas fa-code)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtIconClass" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Proficiency (0-100)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtProficiency" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="GitHub URL" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtGitHubURL" runat="server" CssClass="form-control" TextMode="Url"></asp:TextBox>
            </div>

            <asp:Button ID="btnSave" runat="server" Text="Save Skill" OnClick="btnSave_Click" CssClass="btn btn-primary" />
            <asp:HyperLink runat="server" NavigateUrl="~/Project/Admin.aspx" Text="Cancel" CssClass="btn-cancel" />
            <br />
            <asp:Label ID="lblStatus" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
    </div>
</asp:Content>

