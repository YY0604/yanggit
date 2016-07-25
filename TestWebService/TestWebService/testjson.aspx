<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="testjson.aspx.cs" Inherits="TestWebService.testjson" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
           <asp:label ID="label_Result" runat="server"></asp:label>
            <br />
            <asp:Button ID="ButtonReg" runat="server" Text="测试" OnClick="ButtonReg_Click" />
</asp:Content>

