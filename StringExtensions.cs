public static class StringExtensions
{
    public static IEnumerable<int> IndexesOf(this string str, char symbol)
    {
        var indexes = new List<int>();

        for (int i = 0; i < str.Length; i++)
        {
            if (i == symbol)
                indexes.Add(i);
        }

        return indexes;
    }
}