<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Seller.aspx.cs" Inherits="RealEstateManagement.Seller" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Content/Custom.css" type="text/css" media="screen" />  
    <script>
        //This function will get the marker's current location and then add the lat/long
        //values to our textfields so that we can save the location.
        function markerLocation() {
            //Get location.
            var currentLocation = marker.getPosition();
            var lat = currentLocation.lat();
            var lng = currentLocation.lng();
            document.getElementById('<%=lat.ClientID %>').value = lat;
            document.getElementById('<%=lng.ClientID %>').value = lng;
        }
    </script>
    <div class="jumbotron">
        <input id="pac-input" class="controls" type="text" placeholder="Search Box"><br />
        <div id="map" style="width: 100%; height: 400px; border: 5px solid #5E5454;"></div>
    </div>
    <asp:HiddenField ID="lat" runat="server" />
    <asp:HiddenField ID="lng" runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btn_Add" runat="server" Text="Add Plot" OnClick="Add_Click" CssClass="bluebtn" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
