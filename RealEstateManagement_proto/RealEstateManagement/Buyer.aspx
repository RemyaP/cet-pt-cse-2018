<%@ Page Title="Buyer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Buyer.aspx.cs" Inherits="RealEstateManagement.Buyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        var buyer_map;
        function loadMap() {
            var centerLatLng = new google.maps.LatLng(9.9312328, 76.26730409999999);
            var options = {
                center: centerLatLng,
                zoom: 13,
            }

            buyer_map = new google.maps.Map(document.getElementById('<%=buyer_map.ClientID %>'), options);
            $.ajax({
                type: "GET",
                url: '<%=ResolveUrl("Buyer.aspx/GetPlotDetails")%>',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    var plots = JSON.parse(response.d);
                    for (var i in plots) {
                        if (plots.hasOwnProperty(i)) {
                            var latlng = new google.maps.LatLng(plots[i].Latitude, plots[i].Longitude);
                            var marker = new google.maps.Marker({
                                icon: icon,
                                position: latlng,
                                title: plots[i].PlotId
                            });

                            marker.setMap(buyer_map);
                        }
                    }
                },
                error: function (response) {
                }
            });
        };
        //Load the map when the page has finished loading.
        google.maps.event.addDomListener(window, 'load', loadMap);
        document.bind("projectLoadComplete", loadMap);
    </script>
    <div class="jumbotron">
        <div id="buyer_map" style="width: 100%; height: 400px; border: 5px solid #5E5454;" runat="server">test</div>
    </div>
</asp:Content>
