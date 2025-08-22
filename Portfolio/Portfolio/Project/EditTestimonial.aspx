<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTestimonial.aspx.cs" Inherits="Portfolio.Project.EditTestimonial" MasterPageFile="~/Project/Admin.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Edit Testimonial</h2>
        </div>
        <div class="card-body">
            <div class="mb-3">
                <asp:Label runat="server" Text="Author Name (e.g., Professor Smith)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtAuthorName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Author Title (e.g., CSE Department)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtAuthorTitle" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Author Image URL (e.g., professor.jpg)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtAuthorImageURL" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Quote" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtQuote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>

            <asp:Button ID="btnUpdate" runat="server" Text="Update Testimonial" OnClick="btnUpdate_Click" CssClass="btn btn-primary" />
            <asp:HyperLink runat="server" NavigateUrl="~/Project/Admin.aspx" Text="Cancel" CssClass="btn-cancel" />
            <br />
            <asp:Label ID="lblStatus" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
    </div>
</asp:Content>

