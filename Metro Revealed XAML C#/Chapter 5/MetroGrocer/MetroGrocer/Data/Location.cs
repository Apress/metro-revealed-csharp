using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;

namespace MetroGrocer.Data {
    class Location {

        public static async Task<string> TrackLocation() {
            Geolocator geoloc = new Geolocator();
            Geoposition position = await geoloc.GetGeopositionAsync();

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://nominatim.openstreetmap.org");
            HttpResponseMessage httpResult = await httpClient.GetAsync(
                String.Format("reverse?format=json&lat={0}&lon={1}", 
                position.Coordinate.Latitude, position.Coordinate.Longitude));

            JsonObject jsonObject = JsonObject
                .Parse(await httpResult.Content.ReadAsStringAsync());

            return jsonObject.GetNamedObject("address")
                .GetNamedString("road") + DateTime.Now.ToString("' ('HH:mm:ss')'");
        }
    }
}