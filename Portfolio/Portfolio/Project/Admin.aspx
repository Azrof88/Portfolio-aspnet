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
                        <div class="mb-3 form-check">
    <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" />
    <asp:Label AssociatedControlID="chkRememberMe" runat="server" Text="Remember Me" CssClass="form-check-label"></asp:Label>
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
        
        <h2 id="projects" class="mb-4">Project Management</h2>
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

        <!-- =================================================================== -->
<!-- ================= NEW TESTIMONIALS SECTION ================== -->
<!-- =================================================================== -->
<h2 id="testimonials" class="mb-4">Testimonials Management</h2>
<div class="mb-3">
    <asp:Button ID="btnAddNewTestimonial" runat="server" Text="Add New Testimonial" OnClick="btnAddNewTestimonial_Click" CssClass="btn btn-success" />
</div>
<asp:GridView 
    ID="gvTestimonials" 
    runat="server" 
    AutoGenerateColumns="False" 
    CssClass="table table-hover align-middle"
    DataKeyNames="ID" 
    OnRowDeleting="gvTestimonials_RowDeleting"
    OnRowEditing="gvTestimonials_RowEditing">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" ItemStyle-Width="5%" />
        <asp:BoundField DataField="AuthorName" HeaderText="Author" />
        <asp:TemplateField HeaderText="Quote">
            <ItemTemplate>
                <%-- Show a shortened version of the quote --%>
                <%# Eval("Quote").ToString().Length > 50 ? Eval("Quote").ToString().Substring(0, 50) + "..." : Eval("Quote") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions" ItemStyle-Width="15%">
            <ItemTemplate>
                <asp:LinkButton runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-sm btn-outline-primary me-2" />
                <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Are you sure you want to delete this testimonial?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<!-- =================================================================== -->
<!-- ======================= END OF NEW SECTION ====================== -->
<!-- =================================================================== -->

        <!-- =================================================================== -->
<!-- =================== NEW BLOG MANAGEMENT SECTION ================= -->
<!-- =================================================================== -->
<h2 id="blog" class="mb-4">Blog Management</h2>
<div class="mb-3">
    <asp:Button ID="btnAddNewPost" runat="server" Text="Add New Blog Post" OnClick="btnAddNewPost_Click" CssClass="btn btn-success" />
</div>
<asp:GridView 
    ID="gvBlogPosts" 
    runat="server" 
    AutoGenerateColumns="False" 
    CssClass="table table-hover align-middle"
    DataKeyNames="PostID" 
    OnRowDeleting="gvBlogPosts_RowDeleting"
    OnRowEditing="gvBlogPosts_RowEditing">
    <Columns>
        <asp:BoundField DataField="PostID" HeaderText="ID" ReadOnly="True" ItemStyle-Width="5%" />
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="PublishDate" HeaderText="Published On" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:TemplateField HeaderText="Actions" ItemStyle-Width="15%">
            <ItemTemplate>
                <asp:LinkButton runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-sm btn-outline-primary me-2" />
                <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Are you sure you want to delete this post?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<!-- =================================================================== -->

<!-- =================================================================== -->
<!-- ================= NEW JOURNEY/TIMELINE SECTION ================== -->
<!-- =================================================================== -->
<h2 id="journey" class="mb-4">My Journey Management</h2>
<div class="mb-3">
    <asp:Button ID="btnAddNewEvent" runat="server" Text="Add New Journey Event" OnClick="btnAddNewEvent_Click" CssClass="btn btn-success" />
</div>
<asp:GridView 
    ID="gvTimeline" 
    runat="server" 
    AutoGenerateColumns="False" 
    CssClass="table table-hover align-middle"
    DataKeyNames="ID" 
    OnRowDeleting="gvTimeline_RowDeleting"
    OnRowEditing="gvTimeline_RowEditing">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" ItemStyle-Width="5%" />
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="EventType" HeaderText="Type" />
        <asp:TemplateField HeaderText="Actions" ItemStyle-Width="15%">
            <ItemTemplate>
                <asp:LinkButton runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-sm btn-outline-primary me-2" />
                <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Are you sure you want to delete this event?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<!-- =================================================================== -->
<!-- ======================= END OF NEW SECTION ====================== -->
<!-- =================================================================== -->

       <h2 id="skills" class="mb-4">Skills Management</h2>
        <!-- ADD THIS NEW BUTTON -->
<div class="mb-3">
    <asp:Button ID="btnAddNewSkill" runat="server" Text="Add New Skill" OnClick="btnAddNewSkill_Click" CssClass="btn btn-success" />
</div>
<!-- END OF NEW BUTTON -->
        <%-- You can add a button and GridView for skills here later --%>
        <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="False" 
    CssClass="table table-hover align-middle" DataKeyNames="ID" 
    OnRowDeleting="gvSkills_RowDeleting" OnRowEditing="gvSkills_RowEditing"> <%-- ADD OnRowEditing --%>
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" ItemStyle-Width="5%" />
        <asp:BoundField DataField="SkillName" HeaderText="Skill Name" />
        <asp:BoundField DataField="Proficiency" HeaderText="Proficiency (%)" />
        <asp:TemplateField HeaderText="Actions" ItemStyle-Width="15%">
            <ItemTemplate>
                <!-- ADD THIS EDIT BUTTON -->
                <asp:LinkButton runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-sm btn-outline-primary me-2" />
                <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Are you sure you want to delete this skill?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        <hr class="my-5" />

<!-- =================================================================== -->
<!-- ===================== RECEIVED MESSAGES SECTION =================== -->
<!-- =================================================================== -->
<h2 id="messages" class="mb-4">Received Messages</h2>
<div class="card">
    <div class="card-body">
        <asp:GridView 
            ID="gvMessages" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="table table-hover align-middle mb-0"
            DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="From" ItemStyle-Width="15%" />
                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="20%" />
                <asp:BoundField DataField="Message" HeaderText="Message" />
                <asp:BoundField DataField="DateSent" HeaderText="Received On" DataFormatString="{0:yyyy-MM-dd HH:mm}" ItemStyle-Width="15%" />
            </Columns>
            <EmptyDataTemplate>
                <div class="text-center p-4">
                    No messages received yet.
                </div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</div>
<!-- =================================================================== -->
        </asp:Panel> <!-- <-- THIS IS THE MISSING TAG. ADD IT HERE. -->

</asp:Content>
