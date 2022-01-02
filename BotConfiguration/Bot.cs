using BotConfiguration.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BotConfiguration
{
    public class Bot
    {
        private static string userMessage = "";

        public async Task DiscrodStartClientAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = ReadJson(),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            discord.MessageCreated += async (s, e) =>
            {
                if (isMessageBot(e.Author.IsBot, e.Message.Content, s.CurrentUser.Username))
                {
                    var answer = CheckMessage();
                    await e.Message.RespondAsync(answer);
                }

            };

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });
            commands.RegisterCommands<CommandModule>();

            await discord.ConnectAsync();
            await Task.Delay(-1);

        }

        private static bool isMessageBot(bool isBot, string message, string NameBot)
        {
            string[] check = message.Split(",");
            bool isMessageBot = check[0] == "Бот" || check[0] == NameBot ? true : false;
            if (isMessageBot)
                userMessage = check[1];

            return isMessageBot && !isBot;
        }

        private static string ReadJson()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead(@"C:\Users\danya\source\repos\DiscordBOT\BotConfiguration\config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();
            var myToken = JsonConvert.DeserializeObject<RootJson>(json).Token;
            return myToken;
        }


        private static DiscordMessageBuilder CheckMessage()
        {
            var builder = new DiscordMessageBuilder();

            switch (userMessage.TrimStart(' ').TrimEnd(' ').ToLower())
            {
                case "как дела":
                    return builder.WithContent("Normalin");
                case "че делаешь":
                    return builder.WithContent("Ничего"); ;
                case "проверка":
                    return builder.WithContent("Все работает стабильно");
                case "кнопка":
                    return builder.WithContent("Теперь у меня есть кнопки").AddComponents(CreateButton());
                default:
                    return builder.WithContent("Я хз че это значит");
            }
        }

        private static DiscordButtonComponent CreateButton()
        {
            var myButton = new DiscordButtonComponent(ButtonStyle.Primary, "my_custom_id", "This is a button!");
            return myButton;
        }
    }
}

