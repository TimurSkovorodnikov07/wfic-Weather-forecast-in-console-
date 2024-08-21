using System.Net.Http.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        string pathToConfig = @"../../.config/wfic/config.ngrConfig";

        if (args.Length < 2)
        {
            Writer.ErrorWrite("Ты не указал город и/или колл дней");
            return;
        }
        var cityName = args[0];
        var days = args[1];
        var parseDays = int.Parse(days ?? "0");

        if (string.IsNullOrEmpty(cityName))
        {
            Writer.ErrorWrite("Пустое имя города");
            return;
        }
        else if (string.IsNullOrEmpty(days) || parseDays <= 0 || parseDays > 10)
        {
            Writer.ErrorWrite("Ты не указал колл дней либо превисил лимит в 10 дней");
            return;
        }

        if (!File.Exists(pathToConfig))
        {
            Writer.ErrorWrite("КОнфига в " + pathToConfig + " нету.");
            return;
        }
        var textFromConfig = File.ReadAllText(pathToConfig);
        var conf = ConfigMapper.Map(textFromConfig);

        if (conf is not null)
        {
            try
            {
                var fromNewLine = () => Writer.Write("\n", conf.TextColor);

                Writer.WriteLine($"Hello [color={conf.NameColor}({conf.UserName})]!", conf.TextColor);

                var client = new HttpClient();
                var response = await client.GetAsync($"https://api.weatherapi.com/v1/forecast.json?key={conf.ApiKey}" +
                    $"&q={cityName}&days={days}&aqi=no&alerts=no");

                var content = await response.Content.ReadFromJsonAsync<WeatherApiResponse>();
                var code = (int)response.StatusCode;

                if (content is null)
                {
                    Writer.ErrorWrite("Сервак не вернул данные или вернул но не WeatherApiResponse(в виде json). Код: " + code);
                    return;
                }

                switch (code)
                {
                    case 200:
                        Writer.WriteLine("Прогноз погоды в городе: " + cityName
                            + $" за {days} дней.", conf.TextColor);
                        fromNewLine();

                        foreach (var day in content.forecast.forecastday)
                        {
                            Writer.WriteLine($"[color={conf.DaysColor}({day.date})]", conf.TextColor);

                            int requiredLength = 7;

                            var temperatures = new List<string>();
                            foreach (var hour in day.hour)
                            {
                                var dateTime = DateTime.Parse(hour.time);
                                var hourAndMinutes = $"{dateTime.Hour}:{dateTime.Minute}";

                                var getHourColor = () =>
                                {
                                    var hour = dateTime.Hour;

                                    if (hour > 6 && hour < 16)
                                        return conf.MorningColor;
                                    else if (hour >= 16 && hour < 22)
                                        return conf.EveningColor;
                                    else
                                        return conf.NightColor;
                                };

                                Writer.Write($"[color={getHourColor()}"
                                    + $"({StringPlaceholder.Fill(hourAndMinutes.ToString(), requiredLength)})]"
                                    + $"[color={conf.TextColor}(| )]", conf.TextColor);

                                temperatures.Add(hour.temp_c.ToString());
                            }
                            fromNewLine();

                            //Сделал 2 foreach чтобы сначало в 1-й линии шло время а во 2-й температура
                            foreach (var t in temperatures)
                                Writer.Write($"[color={conf.TemperatureColor}"
                                    + $"({StringPlaceholder.Fill($"{t}°C", requiredLength)})]"
                                    + $"[color={conf.TextColor}(| )]", conf.TextColor);

                            fromNewLine();
                            fromNewLine();
                        }
                        break;
                    case 400:
                        Writer.ErrorWrite($"Города с именем {cityName} нету видимо. Ты написал на латинице и правильно название города?");
                        break;
                    case 401:
                        Writer.ErrorWrite($"Отказано в доступе, проверь токен.");
                        break;
                    default:
                        Writer.ErrorWrite($"Код который я не могу обработать(разрабу было лень писать обработчики): {code}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Writer.ErrorWrite(ex.Message);
            }
        }
        else
        {
            Writer.ErrorWrite("Иди нахуй! Y тебя конфиг не валидный. пиздуй в " +
                @$"""{pathToConfig}""" + ". Чекни на гитхаюе как нужно писать конфиг");
        }
    }
}