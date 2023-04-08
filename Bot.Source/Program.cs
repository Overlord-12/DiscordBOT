using Bot.Configuration;

namespace DiscordBOT
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscorBot bot = new DiscorBot();
            bot.StartDiscrodClientAsync().GetAwaiter().GetResult();
        }
    }
}
