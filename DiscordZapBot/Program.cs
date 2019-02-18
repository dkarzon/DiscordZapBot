using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DiscordZapBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new ZapperBot();
            bot.Run().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
