#pragma warning disable
public class Configuration
{
    [NameToConfig("apiKey")]
    public string ApiKey { get; set; }

    [NameToConfig("userName")]
    public string UserName { get; set; }




    [NameToConfig("morningColor")]
    public int MorningColor { get; set; }

    [NameToConfig("eveningColor")]
    public int EveningColor { get; set; }

    [NameToConfig("nightColor")]
    public int NightColor { get; set; }



    [NameToConfig("daysColor")]
    public int DaysColor { get; set; }

    [NameToConfig("temColor")]
    public int TemperatureColor { get; set; }

    [NameToConfig("nameColor")]
    public int NameColor { get; set; }

    [NameToConfig("textColor")]
    public int TextColor { get; set; }
}