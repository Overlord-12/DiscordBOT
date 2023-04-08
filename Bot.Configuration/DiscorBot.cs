using Bot.Global.Model;
using Bot.Configuration.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Bot.Global;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Bot.Configuration
{
	/// <summary>
	/// Class for initialize discor bot
	/// </summary>
	public class DiscorBot
    {
        private static string m_UserMessage = "";
        private static List<string> m_BanWords = new List<string>();

		/// <summary>
		/// Starts the discrod client asynchronous.
		/// </summary>
		public async Task StartDiscrodClientAsync()
        {
            string botToken = JsonUtils.ReadJson<TokenModelJson>(Constants.BOT_TOKEN_JSON_PATH).Token;
			if (string.IsNullOrEmpty(botToken))
			{
                return;
			}

            UploadBanWords();
            DiscordClient discordClient = new DiscordClient(new DiscordConfiguration()
            {
                Token = botToken,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.DirectMessages
                | DiscordIntents.GuildMessages | DiscordIntents.MessageContents
            });

            discordClient.MessageCreated += async (s, e) =>
            {
                if (IsUseBanWord(e.Message.Content))
                {
                    e.Message.RespondAsync("Осуждаю");
                }

                if (IsMessageBot(e.Author.IsBot, e.Message.Content, s.CurrentUser.Username))
                {
                    DiscordMessageBuilder answer = CheckMessage();
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
            if (!isMessageBot)
			{
                m_UserMessage = check[1];
            }

            return isMessageBot && !isBot;
        }

        private void UploadBanWords()
		{
            BanWordsModelJson banWordsModel = 
                JsonUtils.ReadJson<BanWordsModelJson>(Constants.BAN_WORDS_JSON_PATH);
			foreach (BanWords banWords in banWordsModel.BanWords)
			{
                m_BanWords.Add(banWords.Word);
			}
        }

        private bool IsUseBanWord(string userMessage)
		{
            bool result = m_BanWords.Any(x => userMessage.ToLower()
            .Contains(x.ToLower()));
            return result;
		}

        private DiscordMessageBuilder CheckMessage()
        {
            DiscordMessageBuilder builder = new DiscordMessageBuilder();

            switch (m_UserMessage.TrimStart(' ').TrimEnd(' ').ToLower())
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

