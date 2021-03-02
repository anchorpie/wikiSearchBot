using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Telegram.Bot;

namespace AgainNewBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string token_path = @"C:\Users\Павел\source\My C# Projects\bot_token.txt"; //путь к файлу, где лежит токен для бота

            StreamReader inforb = new StreamReader(token_path, System.Text.Encoding.Default); // Штука для считвания информации из текстового файла

            string token = inforb.ReadToEnd(); // запись текста из файла в переменную

            var botClient = new TelegramBotClient(token); // создается бот с токином

            Console.WriteLine($"Bot {botClient.GetMeAsync().Result} Working..."); // косноль держит вкурсе, что бот работает хоть как-то

            // бота запустили


            botClient.OnMessage += (s, arg) =>
            {
                string userMessage = arg.Message.Text;
                int messageId = arg.Message.MessageId;
                string nickname = arg.Message.Chat.Username;
                long chatId = arg.Message.Chat.Id;

                HttpWebRequest searchOnMessage = (HttpWebRequest)WebRequest.Create($"https://en.wikipedia.org/w/api.php?action=query&list=search&srsearch={userMessage}&format=json"); // Отправляем запрос с поиском

                HttpWebResponse resaltOnMessage = (HttpWebResponse)searchOnMessage.GetResponse(); // Получаем ответ с поиска

                string searchResalt; // Создаем переменную

                using (StreamReader streamReader = new StreamReader(resaltOnMessage.GetResponseStream()))
                {
                    searchResalt = streamReader.ReadToEnd(); // записываем результат поиска в строку
                }

                ResponseJSON responsSearchText = JsonConvert.DeserializeObject<ResponseJSON>(searchResalt); // Дессериализируем поиск

                int PageWikiID = responsSearchText.Query.Search[0].Pageid; // берем ID первой, самой релевантной? страницы из поиска

                // Выполнили поиск


                HttpWebRequest GetPageText = (HttpWebRequest)WebRequest.Create($"https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&pageids={PageWikiID}"); // Отправляем запрос текста к странице

                HttpWebResponse PageTextRaw = (HttpWebResponse)GetPageText.GetResponse(); // Получаем ответ с текстом страницы

                string PageText; // Создаем переменную

                using (StreamReader streamReader = new StreamReader(PageTextRaw.GetResponseStream()))
                {
                    PageText = streamReader.ReadToEnd(); // записываем текст страницы в строку
                }

                var responseText = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, PageData>>>>(PageText);// Диссериализируем через словари

                var extract = responseText["query"].FirstOrDefault().Value.FirstOrDefault().Value.Extract; // Достаем из словарей нужный текст

                // Достали текст со страницы


                botClient.SendTextMessageAsync(
                       chatId: chatId,
                       text: "Вот че по запросу нашел:  " + extract
                       ); // Отправили сообщение

                using (StreamWriter UsersLogWriter = new StreamWriter(@"C:\Users\Павел\source\My C# Projects\txtFiles\UsersLog.txt", true)) // Записываем лог всех пользователей
                {
                    UsersLogWriter.WriteLine($" Пользователь {nickname} отправил сообщение: {userMessage}");
                }

                Console.WriteLine($" Сообщение пользователя: {nickname}, записанно в файл"); // Пишем в консоль, что текст записан

            };

            botClient.StartReceiving();
            Console.ReadLine();
        }
    }
}
