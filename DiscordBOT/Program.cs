using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using Newtonsoft.Json;
using DiscordBotLibrary;

namespace DiscordBOT
{
    class Program
    {

        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.DiscrodClientConnectAsync().GetAwaiter().GetResult();
           
        }


    }
}
