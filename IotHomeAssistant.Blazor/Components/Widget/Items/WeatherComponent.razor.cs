using IoTHomeAssistant.Domain.Enums;
using OpenWeatherMap.Core.Enums;
using OpenWeatherMap.Core.Models;
using OpenWeatherMap.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class WeatherComponent
    {
        private const string APP_KEY = "9c25b6c6b12f8ad20a25e9dd63d0f80f";

        private static List<string> Days = new List<string>() { "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя" };
        private static List<string> ShortDays = new List<string>() { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "НД" };
        private static List<string> Months = new List<string>() { "Січня", "Лютого", "Березня", "Квітня", "Травня", "Червня", "Липня", "Серпня", "Вересня", "Жовтня", "Листопада", "Грудня" };

        private OpenWeatherMap.Core.OpenWeatherMap openWeatherMap;

        protected List<Daily> dailyWeather = new List<Daily>();
        protected CurrentWeatherModel currentWeather;

        protected int minTemp = 0;
        protected int maxTemp = 0;

        protected DateTime dateTime = DateTime.Now;
        protected int dayOfWeek = 0;

        protected override async Task OnInitializedAsync()  
        {
            if (WidgetItem.Type == WidgetItemTypeEnum.WeatherForecast && 
                WidgetItem.Latitude.HasValue && WidgetItem.Longitude.HasValue)
            {
                openWeatherMap = new OpenWeatherMap.Core.OpenWeatherMap(APP_KEY);
                currentWeather = await openWeatherMap.QueryAsync<CurrentWeatherModel>(
                    WidgetItem.Latitude.Value,
                    WidgetItem.Longitude.Value,
                    units: Units.Metric,
                    language: Language.Ukrainian);

                var weather = await openWeatherMap.QueryAsync<OneCallWeatherModel>(
                    WidgetItem.Latitude.Value, 
                    WidgetItem.Longitude.Value, 
                    units: Units.Metric, 
                    language: Language.Ukrainian);

                if (weather != null && weather.Daily.Count > 4)
                {
                    dailyWeather = weather.Daily.GetRange(1, 4);
                    var first = weather.Daily.First();
                    minTemp = (int)first.Temp.Min;
                    maxTemp = (int)first.Temp.Max;
                }

                dayOfWeek = (int)dateTime.DayOfWeek;
            }
        }
    }
}
