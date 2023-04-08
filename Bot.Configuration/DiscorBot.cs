using Bot.Global.Model;
using Bot.Configuration.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Configuration
{
	/// <summary>
	/// Class for initialize discor bot
	/// </summary>
	public class DiscorBot
    {
        private static string USER_MESSAGE = "";

		/// <summary>
		/// Starts the discrod client asynchronous.
		/// </summary>
		public async Task StartDiscrodClientAsync()
        {
            DiscordClient discordClient = new DiscordClient(new DiscordConfiguration()
            {
                Token = ReadJson(),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            discordClient.MessageCreated += async (s, e) =>
            {
                if (IsMessageBot(e.Author.IsBot, e.Message.Content, s.CurrentUser.Username))
                {
                    var answer = CheckMessage();
                    await e.Message.RespondAsync(answer);
                }

            };

            CommandsNextExtension commands = discordClient.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });

            commands.RegisterCommands<CommandModule>();
            await discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static bool IsMessageBot(bool isBot, string message, string NameBot)
        {
            string[] check = message.Split(",");
            bool isMessageBot = check[0] == "Бот" || check[0] == NameBot ? true : false;
            if (isMessageBot)
			{
                USER_MESSAGE = check[1];
            }

            return isMessageBot && !isBot;
        }

        private static string ReadJson()
        {
            string json = string.Empty;
            using (var fs = File.OpenRead(@""))
			{
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = sr.ReadToEnd();
                }
            }
               
            string myToken = JsonConvert.DeserializeObject<RootJson>(json).Token;
            return myToken;
        }


        private static DiscordMessageBuilder CheckMessage()
        {
            DiscordMessageBuilder builder = new DiscordMessageBuilder();
            switch (USER_MESSAGE.TrimStart(' ').TrimEnd(' ').ToLower())
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
                    return builder.WithContent("I dont'now what is it");
            }
        }

        private static DiscordButtonComponent CreateButton()
        {
            DiscordButtonComponent myButton = new DiscordButtonComponent(ButtonStyle.Primary, "my_custom_id", "This is a button!");
            return myButton;
        }
    }
}

