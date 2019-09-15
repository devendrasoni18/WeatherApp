<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WeatherDetails.aspx.cs" Inherits="WeatherDetailsApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <p class="lead">
            <asp:Label ID="lblHeader" runat="server" Text="Please Enter the details below to know the weather"></asp:Label>
        </p>
        <p class="lead">
            <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            <asp:Label ID="lblError" runat="server" Text="Please Enter a valid City Name" Visible="False" ForeColor="#CC0000"></asp:Label>
        </p>
        <p class="lead">
            <asp:Literal ID="ltTable" runat="server" />
        </p>

        <p class="lead">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Reset" OnClick="btnBack_Click" Visible="False" />
        </p>
    </div>
</asp:Content>
