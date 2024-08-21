using System.Text;

public class StringPlaceholder
{
    public static string Fill(string str, int requiredLength)
    {
        if (string.IsNullOrEmpty(str))
            throw new ArgumentNullException();
        else if (str.Length > requiredLength)
            throw new Exception($"У тебя строка длины {str.Length}, когда требуеться {requiredLength}");

        var strLength = str.Length;
        var spaces = new StringBuilder("");
        while (strLength + spaces.Length < requiredLength)
            spaces.Append(" ");

        return str + spaces.ToString();
    }
}