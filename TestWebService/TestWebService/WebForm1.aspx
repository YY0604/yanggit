<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TestWebService.WebForm1" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
           <asp:label ID="label_Result" runat="server"></asp:label>
            <br />
            <asp:Button ID="ButtonReg" runat="server" Text="获取数据插入新表" OnClick="ButtonReg_Click" />
</asp:Content>
