<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Portfolio.Project.Admin" MasterPageFile="~/Project/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- Login Panel -->
    <asp:Panel ID="LoginPanel" runat="server" Visible="true">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <div class="card mt-5">
                    <div class="card-body p-4">
                        <h3 class="card-title text-center mb-4">Admin Login</h3>
                        <div class="mb-3">
                            <asp:Label runat="server" Text="Username" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" Text="Password" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                        </div>
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3 d-block text-center"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- Admin Content Panel -->
    <asp:Panel ID="AdminContentPanel" runat="server" Visible="false">
        <h2 class="mb-4">Project Management</h2>
        <div class="mb-3">
            <asp:Button ID="btnAddNewProject" runat="server" Text="Add New Project" OnClick="btnAddNewProject_Click" CssClass="btn btn-success" />
        </div>
        <asp:GridView 
            ID="gvProjects" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="table table-hover align-middle"
            DataKeyNames="ID" 
            OnRowDeleting="gvProjects_RowDeleting"
            OnRowEditing="gvProjects_RowEditing">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" ItemStyle-Width="5%" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Category" HeaderText="Category" />
                <asp:TemplateField HeaderText="Actions" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-sm btn-outline-primary me-2" />
                        <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Are you sure you want to delete this project?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <hr class="my-5" />

        <h2 class="mb-4">Skills Management</h2>
        <%-- You can add a button and GridView for skills here later --%>
        <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-middle" DataKeyNames="ID" OnRowDeleting="gvSkills_RowDeleting">
             <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" ItemStyle-Width="5%" />
                <asp:BoundField DataField="SkillName" HeaderText="Skill Name" />
                <asp:BoundField DataField="Proficiency" HeaderText="Proficiency (%)" />
                <asp:TemplateField HeaderText="Actions" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <%-- Add Edit button for skills later if needed --%>
                        <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Are you sure you want to delete this skill?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
