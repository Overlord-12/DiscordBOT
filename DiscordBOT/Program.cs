using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using Newtonsoft.Json;
using BotConfiguration;

namespace DiscordBOT
{
    class Program
    {
        private static string userMessage = "";

        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.DiscrodStartClientAsync().GetAwaiter().GetResult();
        }

    }

}
