using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using static System.Net.Mime.MediaTypeNames;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Enums;
using System.IO;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace NulesIT_Bot
{

    class Program
    {
        private static TelegramBotClient botClient = new TelegramBotClient("************") { Timeout = TimeSpan.FromSeconds(10) };
        static void Main(string[] args)
        {                        

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot id:{me.Id}. Bot Name: {me.FirstName} ");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();



            Console.ReadKey();
        }

       
        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            /////////////////// Что бы видеть смс пользователей в консоли /////////////////////////////////////
            var text = e?.Message.Text;
            if (text == null) 
                return;
            Console.WriteLine($"recived text message '{text}' in chat '{e.Message.Chat.Id}'");

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            if (e.Message == null || e.Message.Type != MessageType.Text) return;


            switch (e.Message.Text.Split(' ').First())
            {
                case "/Help":
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Здравствуйте " + e.Message.Chat.Username + "\n"+ @"Что вас интересует? :

/News Новости факультета
/EducationalProcess Графики учебного процесса.
/CouplesSchedule Рассписание пар в НУБиП.
/PairSchedule Рассписание занятий.
/Scholarship Стипендия.
/ExtraPoints За что можно получить дополнительные балл? 
/SportsSections Спортивные секции.
/Chairs Кафедры факультета.
/Telegram_IT Канал факультета в Telegram
/Telegram_NUBIP Канал университета в Telegram
/Useful Случайный факт из мира IT.
                                        ");
                    break;

                case "/start":
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Здравствуйте " + e.Message.Chat.Username + "\n" + @"Что вас интересует? :

/News Новости факультета
/CouplesSchedule Рассписание пар в НУБиП.
/PairSchedule Рассписание занятий.
/EducationalProcess Графики учебного процесса.
/ExtraPoints За что можно получить дополнительные балл?
/Scholarship Стипендия.
/SportsSections Спортивные секции.
/Chairs Кафедры факультета.
/Telegram_IT Канал факультета в Telegram
/Telegram_NUBIP Канал университета в Telegram
/Useful Случайный факт из мира IT.
                                        ");
                    break;


                case "/News": //Новинки происходящие на факультете
                    await botClient.SendChatActionAsync(e.Message.Chat.Id, ChatAction.UploadPhoto);
                    const string News = @"D:\Projekts\NulesIT_Bot\NulesIT_Bot\Image\News\photo_2019-11-11_10-12-57.jpg";
                    var news = News.Split(Path.DirectorySeparatorChar).Last();
                    using (var fileStream = new FileStream(News, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await botClient.SendPhotoAsync(
                            e.Message.Chat.Id,
                            fileStream);
                    }
                    break;



                case "/CouplesSchedule": //Список звонков
                    await botClient.SendChatActionAsync(e.Message.Chat.Id, ChatAction.UploadPhoto);
                    const string file = @"D:\Projekts\NulesIT_Bot\NulesIT_Bot\Image\CouplesSchedule.jpg";
                    var fileName = file.Split(Path.DirectorySeparatorChar).Last();
                    using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await botClient.SendPhotoAsync(
                            e.Message.Chat.Id,
                            fileStream);
                    }
                    break;

                case "/PairSchedule": //Расписание занятий
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "По этой ссылке вы можете детально ознакомиться с рассписанием занятий:\n" +
                    "https://nubip.edu.ua/node/2969/6");
                    break;
               
                case "/EducationalProcess": //График учебного процесса
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "По этой ссылке вы можете детально ознакомиться с графиками учебного процесса:\n" +
                    "https://nubip.edu.ua/node/2969/4");
                    break;
               
                case "/ExtraPoints": //Доп Балы
                    await botClient.SendChatActionAsync(e.Message.Chat.Id, ChatAction.UploadDocument);
                    const string bals = @"D:\Projekts\NulesIT_Bot\NulesIT_Bot\File\DOP_BAL.docx";
                    var filePoints = bals.Split(Path.DirectorySeparatorChar).Last();
                    using (var filepdf = new FileStream(bals, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await botClient.SendDocumentAsync(
                            e.Message.Chat.Id,
                            filepdf, "В этом документе Вы найдёте подробную информации о дополнительных баллах.");
                    }
                    break;

                #region Стипендия
                case "/Scholarship": //Стипендия
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, @"Что вас интересует:

 /WhereisMyMoney Когда происходит начисление стипендии?
 /SocialScholarship Социальная стипендия.
                    ");
                    break;

                case "/WhereisMyMoney":   //Начисление обычной стипендии          
                        await botClient.SendTextMessageAsync(e.Message.Chat.Id, "28.12.2019");
                    break;
                

                case "/SocialScholarship": // Соц. Степендия                   
                        await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Со всеми деталями можно ознакомиться на сайте НУБиП: \n https://nubip.edu.ua/node/12433/1");
                    
                    break;
                #endregion

                case "/SportsSections": //Спортивные секции
                        await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Всю информацию о спорте, Вы можете найти на сайте:\n https://nubip.edu.ua/node/4220");
                                
                    break;
                
                case "/Telegram_IT": //Канал факультета в телеграме
                        await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://t.me/it_nubip");
                                
                    break;
                
                case "/Telegram_NUBIP": //Канал университета в телеграме
                        await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://t.me/nubip1898");
                                
                    break;

                case "/Chairs":
                    var keyboard = new InlineKeyboardMarkup(new[]
{
    new [] // first row
    {
        InlineKeyboardButton.WithUrl("Інформаційних і дистанційних технологій","https://nubip.edu.ua/node/3900"),
        InlineKeyboardButton.WithUrl("Комп’ютерних систем і мереж","https://nubip.edu.ua/node/3713"),
    },
    new [] // second row
    {
        InlineKeyboardButton.WithUrl("Інформаційних систем","https://nubip.edu.ua/node/2970"),
        InlineKeyboardButton.WithUrl("Комп'ютерних наук","https://nubip.edu.ua/node/2972"),
        InlineKeyboardButton.WithUrl("Економічної кібернетики","https://nubip.edu.ua/node/2971"),

    }
    
});
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Кафедри факультету", replyMarkup: keyboard);
                    
                    break;
                                    

                #region Плюшки
                case "/Useful": //Бонус
                    Random plushka = new Random();
                  int item = plushka.Next(0, 6);

                    switch (item)
                    {
                        case 0:

                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://telegra.ph/MacBook-dlya-programmista-Stoit-li-11-08");

                            break;

                        case 1:
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://telegra.ph/5-yazykov-programmirovaniya-kotorye-budut-vostrebovany-v-2020-godu-11-23");
                            
                            break;
                        
                        case 2:
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://telegra.ph/5-prichin-pochemu-stoit-zanyatsya-frilansom-posle-uchyoby-11-23");
                            
                            break;
                        
                        case 3:
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://telegra.ph/Ckolko-zarabatyvayut-programmisty-v-Ukraine-i-gde-ehtomu-uchatsya-11-23");
                            
                            break;
                        
                        case 4:
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://telegra.ph/Kak-popast-na-rabotu-v-Google-6-sovetov-ot-byvshih-sotrudnikov-11-23");
                            
                            break;
                        
                        case 5:
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://telegra.ph/Samye-poleznye-programmy-dlya-Windows-10-11-23");
                            
                            break;
                        
                        case 6:
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "https://telegra.ph/9-privychek-kotorye-vedut-k-razocharovaniyu-v-zhizni-11-23");
                            
                            break;

                        default:
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Error");
                            break;
                    }


                 break;





                #endregion

                
                default:
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, @"Извините, мы не можем вам помочь по этому запросу. Воспользуйтесь командой /Help что бы узнать список доступных команд");
                    break;
            }                      

        }
    }
}