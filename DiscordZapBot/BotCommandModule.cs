using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DiscordZapBot
{
    public class BotCommandModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync($"👋 pong, {Context.User.Mention}!");
        }

        [Command("zap")]
        public async Task Zap()
        {
            await ReplyAsync(ZapQuote.RandomQuote());
        }

        [Command("echo")]
        public Task EchoAsync([Remainder] string text)
            // Insert a ZWSP before the text to prevent triggering other bots!
            => ReplyAsync('\u200B' + text);
    }
}
