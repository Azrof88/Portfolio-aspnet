<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTimelineEvent.aspx.cs" Inherits="Portfolio.Project.AddTimelineEvent" MasterPageFile="~/Project/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Add New Journey Event</h2>
        </div>
        <div class="card-body">
            <div class="mb-3">
                <asp:Label runat="server" Text="Event Type" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlEventType" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Education" Value="Education"></asp:ListItem>
                    <asp:ListItem Text="Experience" Value="Experience"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Title (e.g., BSc in Computer Science)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Institution (e.g., Your University)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtInstitution" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Date Range (e.g., 2021 - Present)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDateRange" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Description" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>

            <asp:Button ID="btnSave" runat="server" Text="Save Event" OnClick="btnSave_Click" CssClass="btn btn-primary" />
            <asp:HyperLink runat="server" NavigateUrl="~/Project/Admin.aspx" Text="Cancel" CssClass="btn-cancel" />
            <br />
            <asp:Label ID="lblStatus" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
    </div>
</asp:Content>

