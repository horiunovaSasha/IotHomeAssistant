using IotHomeAssistant.Blazor.Data;
using IoTHomeAssistant.Domain.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Edit
{
    public partial class EditWeatherItemComponent
    {
        [Parameter]
        public WidgetItemDto WidgetItem { get; set; }

        [Inject] 
        public IJSRuntime jsRuntime { get; set; }

        protected GeoInfo geoInfo = new GeoInfo();

        protected override async Task OnInitializedAsync()
        {
            if (WidgetItem.Latitude.HasValue && WidgetItem.Longitude.HasValue)
            {
                geoInfo.Latitude = WidgetItem.Latitude.Value;
                geoInfo.Longitude = WidgetItem.Longitude.Value;
            }
            else
            {
                geoInfo = await GetGeoInfo();
            }
        }

        public void OnMapClickEvent(Syncfusion.Blazor.Maps.MouseEventArgs args)
        {
            geoInfo.Latitude = args.Latitude;
            geoInfo.Longitude = args.Longitude;

            WidgetItem.Latitude = args.Latitude;
            WidgetItem.Longitude = args.Longitude;
        }

        private async Task<string> GetIPAddressAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://api.ipify.org?format=jsonp");

                if (response.IsSuccessStatusCode)
                {
                    return (await response.Content.ReadAsStringAsync())
                        .Replace("callback({\"ip\":\"", string.Empty)
                        .Replace("\"});", string.Empty);
                }
            }
            catch
            {
            }

            return string.Empty;
        }

        public async Task<GeoInfo> GetGeoInfo()
        {
            var ip = await GetIPAddressAsync();

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"http://api.ipstack.com/{ip}?access_key=c85e1f9c316d09e93767f70a99600421");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<GeoInfo>(json);
                }
            }
            catch
            {
            }

            return null;
        }
    }
}