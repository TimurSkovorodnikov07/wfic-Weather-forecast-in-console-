public static class ConfigParser
{
    public static Dictionary<string, string> Parse(string text)
    {
        var result = new Dictionary<string, string>();

        var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var currentComment = string.Empty;

        foreach (var line in lines)
        {
            //Убираем пробелы
            var trimmedLine = line.Trim();

            // Определяем комментарии
            if (trimmedLine.StartsWith("//"))
            {
                currentComment = trimmedLine.Substring(2).Trim();
                continue;
            }

            // Пропускаем пустые строки
            if (string.IsNullOrWhiteSpace(trimmedLine))
                continue;

            // Парсим ключ и значение
            var parts = trimmedLine.Split('=');
            if (parts.Length == 2)
            {
                var key = parts[0].Trim();
                var value = parts[1].Trim().Trim(';').Trim();

                result.Add(key, value);
            }
        }
        return result;
    }
}
