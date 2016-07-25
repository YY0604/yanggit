<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestHTTP.aspx.cs" Inherits="TestWebService.TestHTTP" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
           <asp:label ID="label_Result" runat="server"></asp:label>
            <br />
            <asp:Button ID="ButtonReg" runat="server" Text="httpwebrequest" OnClick="ButtonReg_Click" />
</asp:Content>