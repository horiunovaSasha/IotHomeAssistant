using System;
using System.Drawing;
using System.Threading.Tasks;

namespace YeeLightPlugin
{
    public class YeeLightDevice
    {
        public static async Task Exec(Command command)
        {
            try
            {
                var light = new YeelightAPI.Device(VariableExtension.IP_ADDR, 55443);
                if (await light.Connect())
                {
                    if (command.Toggle)
                    {
                        await light.SetPower(true);
                        await light.SetBrightness(command.Brightness);

                        if (!string.IsNullOrWhiteSpace(command.Color))
                        {
                            var rgb = ColorTranslator.FromHtml(command.Color);
                            await light.SetRGBColor(rgb.R, rgb.G, rgb.B);
                        }
                    }
                    else
                    {
                        await light.SetPower(false);
                    }

                    light.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<Command> GetStatus()
        {
            try
            {
                var light = new YeelightAPI.Device(VariableExtension.IP_ADDR, 55443);
                if (await light.Connect())
                {
                    var response = new Command()
                    {
                        Toggle = light.Properties.ContainsKey("power") ? light.Properties["power"].ToString() == "on" : false,
                        Brightness = light.Properties.ContainsKey("bright") ? int.Parse(light.Properties["bright"].ToString()) : 100,
                        Color = light.Properties.ContainsKey("rgb") ? GetHtmlColor(light.Properties["rgb"]) : string.Empty
                    };

                    light.Disconnect();

                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private static string GetHtmlColor(object rgb)
        {
            var color = ColorTranslator.FromOle(int.Parse(rgb.ToString()));
            var html = ColorTranslator.ToHtml(color); //#5578fa

            return $"#{html.Substring(5, 2)}{html.Substring(3,2)}{html.Substring(1,2)}";
        }
    }
}
