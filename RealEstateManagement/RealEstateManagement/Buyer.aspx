<%@ Page Title="Buyer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Buyer.aspx.cs" Inherits="RealEstateManagement.Buyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $.ajax({
            type: "GET",
            url: '<%=ResolveUrl("Buyer.aspx/GetPlotDetails")%>',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async : false,
            success: function (response) {
                var plots = JSON.parse(response.d);
                for (var i in plots) {
                    if (plots.hasOwnProperty(i)) {
                        alert(JSON.stringify(plots[i].Latitude));
                        alert(JSON.stringify(plots[i].Longitude));
                    }
                }
            },
            error: function (response) {
            }
        });
        
        var buyer_map;
        function loadMap() {
            alert("test1");
            var options = {
                center: { lat: 9.9312328, lng: 76.26730409999999 },
                zoom: 13,
                //mapTypeId: 'roadmap'
            }
            buyer_map = new google.maps.Map(document.getElementById('buyer-map'), options);
        };
        //Load the map when the page has finished loading.
        google.maps.event.addDomListener(window, 'load', loadMap);
        document.bind("projectLoadComplete", loadMap);
    </script>
    <div class="jumbotron">
        <div id="buyer-map" style="width: 100%; height: 400px; border: 5px solid #5E5454;"></div>
    </div>
</asp:Content>
