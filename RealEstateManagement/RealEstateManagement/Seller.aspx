<%@ Page Title="Seller" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Seller.aspx.cs" Inherits="RealEstateManagement.Seller" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Content/Custom.css" type="text/css" media="screen" />
    <script>
        //Set up some of our variables.
        var map; //Will contain map object.
        var marker = false; ////Has the user plotted their location marker? 

        function initMap() {
            var options = {
                center: { lat: 9.9312328, lng: 76.26730409999999 },
                zoom: 13,
                //mapTypeId: 'roadmap'
            }
            map = new google.maps.Map(document.getElementById('map'), options);

            // Create the search box and link it to the UI element.
            var input = document.getElementById('pac-input');
            var searchBox = new google.maps.places.SearchBox(input);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

            // Bias the SearchBox results towards current map's viewport.
            map.addListener('bounds_changed', function () {
                searchBox.setBounds(map.getBounds());
            });

            var markers = [];
            // Listen for the event fired when the user selects a prediction and retrieve
            // more details for that place.
            searchBox.addListener('places_changed', function () {
                var places = searchBox.getPlaces();

                if (places.length == 0) {
                    return;
                }

                // Clear out the old markers.
                markers.forEach(function (marker) {
                    marker.setMap(null);
                });
                markers = [];

                // For each place, get the icon, name and location.
                var bounds = new google.maps.LatLngBounds();
                places.forEach(function (place) {
                    if (!place.geometry) {
                        console.log("Returned place contains no geometry");
                        return;
                    }

                    // Create a marker for each place.
                    markers.push(new google.maps.Marker({
                        map: map,
                        icon: icon,
                        title: place.name,
                        position: place.geometry.location
                    }));

                    if (place.geometry.viewport) {
                        // Only geocodes have viewport.
                        bounds.union(place.geometry.viewport);
                    } else {
                        bounds.extend(place.geometry.location);
                    }
                });
                map.fitBounds(bounds);
            });

            //Pick location
            //Listen for any clicks on the map.
            google.maps.event.addListener(map, 'click', function (event) {
                //Get the location that the user clicked.
                var clickedLocation = event.latLng;
                //If the marker hasn't been added.
                if (marker === false) {
                    //Create the marker.
                    marker = new google.maps.Marker({
                        icon: icon,
                        position: clickedLocation,
                        map: map,
                        draggable: true //make it draggable
                    });
                    //Listen for drag events!
                    google.maps.event.addListener(marker, 'dragend', function (event) {
                        markerLocation();
                    });
                } else {
                    //Marker has already been added, so just change its location.
                    marker.setPosition(clickedLocation);
                }
                //Get the marker's location.
                markerLocation();
            });
        }
        
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

        //Load the map when the page has finished loading.
        google.maps.event.addDomListener(window, 'load', initMap);
        document.bind("projectLoadComplete", initMap);
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
