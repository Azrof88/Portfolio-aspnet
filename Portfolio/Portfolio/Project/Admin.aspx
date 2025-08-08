<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Admin.aspx.cs" CodeFile="~/Project/Admin.aspx.cs" Inherits="Portfolio.Project.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Panel</title>
    <link rel="stylesheet" type="text/css" href="css/admin.css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <!-- Login Panel: This will be visible to users who are NOT logged in -->
        <asp:Panel ID="LoginPanel" runat="server" CssClass="login-panel" Visible="true">
            <h2 class="admin-header">Admin Login</h2>
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />
            <br />
            <asp:Label ID="lblError" runat="server" CssClass="lbl-error" ForeColor="Red"></asp:Label>
        </asp:Panel>

        <!-- Admin Content Panel: This will be visible ONLY to logged-in users -->
        <asp:Panel ID="AdminContentPanel" runat="server" CssClass="admin-container" Visible="false">
            <h2 class="admin-header">Project Management</h2>
            <p>Welcome, Admin! Here you can manage your portfolio projects.</p>
            
            <!-- We will add the project grid here in the next phase -->

        </asp:Panel>

    </form>
</body>
</html>
