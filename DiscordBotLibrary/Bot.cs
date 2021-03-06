using DiscordBOT;
using DSharpPlus;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotLibrary
{
    public class Bot
    {
        private static string userMessage = "";

       public async Task DiscrodClientConnectAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = ReadJson(),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            discord.MessageCreated += async (s, e) =>
            {

                if (isMessageBot(e.Author.IsBot, e.Message.Content))
                {
                    var answer = CheckMessage();
                    await e.Message.RespondAsync(answer);
                }

            };


            await discord.ConnectAsync();
            await Task.Delay(-1);

        }

        private static bool isMessageBot(bool isBot, string message)
        {
            string[] check = message.Split(",");
            bool isMessageBot = check[0] == "Бот" ? true : false;
            if (isMessageBot)
                userMessage = check[1];

            return isMessageBot && !isBot;
        }

        private static string ReadJson()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead(@"C:\Users\danya\source\repos\DiscordBOT\DiscordBOT\config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();
            var myToken = JsonConvert.DeserializeObject<RootJson>(json).Token;
            return myToken;
        }


        private static string CheckMessage()
        {
            switch (userMessage.TrimStart(' ').TrimEnd(' ').ToLower())
            {
                case "как дела":
                    return "Normalin";
                case "че делаешь":
                    return "Как обычно, ничего";
                default:
                    return "Я хз че это значит";
            }
        }
    }
}
