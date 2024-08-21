---------------------Что нужно-----------------------:
Желательна иметь поддержку Nerd fonts в твоем терминале

---------------------Как юзать-----------------------:
sh .../wfic.sh "CityName" "Days"

Колл дней не меньше 1 и больше 10
Example: sh /home/timur/wfic/wfic.sh "Tokyo" "7"

---------------------Пример конфига------------------:
//Путь к конфигу должен быть таким /home/xxx/.config/wfic/config.ngrConfig
//Коменты писать строго в отдельных линиях и как показано тут
textColor=5;
daysColor=10;
temColor=11;
nameColor=10;
interval=4;

nightColor=1;
eveningColor=6;
morningColor=14;

userName=Timur;
apiKey=xxx;
//Тут твой apiKey из https://www.weatherapi.com/

//Только желательно не юзай крастный и желтый, их юзает приложение когда выводит warn и error
//Хотя знаешь, похуй, забей, если будет ошибка сразу поймешь.
// DarkBlue = 1,
// Black = 0,
// DarkGreen = 2,
// DarkCyan = 3,
// DarkRed = 4,
// DarkMagenta = 5,
// DarkYellow = 6,
// Gray = 7,
// DarkGray = 8,
// Blue = 9,
// Green = 10,
// Cyan = 11,
// Red = 12,
// Magenta = 13,
// Yellow = 14,
// White = 15
