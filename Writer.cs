using System.Text.RegularExpressions;

//Код благополучно спиздил у chatGPT потому что я инвалид в работе со строками, regex для меня это пиздец вобще.
public static class Writer
{
    public static void Write(string text, int defColor) => Render(Parse(text), false, defColor);
    public static void WriteLine(string text, int defColor) => Render(Parse(text), true, defColor);
    public static void WarningWrite(string text) => Render(Parse($"[color=6({text})]"), true);
    public static void ErrorWrite(string text) => Render(Parse($"[color=4({text})]"), true);


    private static List<TextAndColor> Parse(string input)
    {
        var styledTexts = new List<TextAndColor>();
        var regex = new Regex(@"\[color=(\d+)\((.*?)\)\]");
        var matches = regex.Matches(input);

        int lastIndex = 0;

        foreach (Match match in matches)
        {
            int color = int.Parse(match.Groups[1].Value);
            string text = match.Groups[2].Value;

            if (match.Index > lastIndex)
            {
                styledTexts.Add(new TextAndColor
                (
                    input.Substring(lastIndex, match.Index - lastIndex),
                    0 // Default color
                ));
            }

            styledTexts.Add(new TextAndColor(text, color));

            lastIndex = match.Index + match.Length;
        }

        if (lastIndex < input.Length)
            styledTexts.Add(new TextAndColor(input.Substring(lastIndex), 0));

        return styledTexts;
    }
    private static void Render(List<TextAndColor> styledTexts, bool writingToNewLine, int defColor = 0)
    {
        foreach (var styledText in styledTexts)
        {
            var color = styledText.Color;
            Console.ForegroundColor = (ConsoleColor)(color is not 0 ? color : defColor);
            Console.Write(styledText.Text);
        }

        if (writingToNewLine)
            Console.Write("\n");
    }
}