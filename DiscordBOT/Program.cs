using System;
using System.Threading.Tasks;
using DSharpPlus;

namespace DiscordBOT
{
    class Program
    {
        private static string userMessage = "";

        static void Main(string[] args)
        {
            DiscrodClientAsync().GetAwaiter().GetResult();
        }

        static async Task DiscrodClientAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            discord.MessageCreated += async (s, e) =>
            {

                if(isMessageBot(e.Author.IsBot,e.Message.Content))
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
