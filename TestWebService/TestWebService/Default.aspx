<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestWebService._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    City：<asp:TextBox ID="textBox_CityName" runat="server"></asp:TextBox>
            <br />
           <asp:label ID="label_Result" runat="server"></asp:label>
            <br />
            <asp:Button ID="ButtonReg" runat="server" Text="查找" OnClick="ButtonReg_Click" />
</asp:Content>

