using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordZapBot
{
    public class ZapperBot
    {
        //////////////////////////////////////////////////////////////////
        // Update these variables with your bot's client id and token
        //////////////////////////////////////////////////////////////////
        private string _clientId = "<clientId>";
        private string _botToken = "<botToken>";

        // Use this invite url (with your clientid) to add the bot to your Discord guild
        // Invite url: https://discordapp.com/api/oauth2/authorize?client_id=<clientid>&scope=bot&permissions=3145728

        private ServiceProvider _services;
        private DiscordSocketClient _discord;
        private CommandService _commands;

        private Random _random = new Random();
        private TimeSpan _minTimeBetweenSays = TimeSpan.FromMinutes(5);

        private Dictionary<ulong, DateTime> _lastZapped = new Dictionary<ulong, DateTime>();
        private Dictionary<ulong, IAudioClient> _connections = new Dictionary<ulong, IAudioClient>();

        public async Task Run()
        {
            Console.WriteLine(ZapQuote.RandomQuote());
            _services = ConfigureServices();

            _discord = _services.GetRequiredService<DiscordSocketClient>();
            
            _commands = new CommandService();

            _discord.Log += LogAsync;

            _discord.UserVoiceStateUpdated += OnVoiceStateUpdated;

            await _discord.LoginAsync(TokenType.Bot, _botToken);
            await _discord.StartAsync();

            await _services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            // Keep the thing running...
            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(1));

                // Trigger an update on voice connections
                await BrannigansLaw();
            }
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .BuildServiceProvider();
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private async Task BrannigansLaw()
        {
            // Brannigan's law is like Brannigan's love, hard and fast
            foreach (var g in _discord.Guilds)
            {
                try
                {
                    if (_connections.TryGetValue(g.Id, out var connection))
                    {
                        if (!ShouldZap(g))
                            continue;

                        await RandomSound(connection);
                        _lastZapped[g.Id] = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine($"- {ex.StackTrace}");
                }
            }
        }

        private bool ShouldZap(IGuild g)
        {
            if (_lastZapped.TryGetValue(g.Id, out var lastZap) && (DateTime.Now - lastZap) < _minTimeBetweenSays)
            {
                return false;
            }

            return _random.Next(100) >= 90;
        }

        private async Task RandomSound(IAudioClient connection)
        {
            await Say(connection, ZapSound.RandomSound());
        }
        
        private async Task Say(IAudioClient connection, ZapSound sound)
        {
            try
            {
                await connection.SetSpeakingAsync(true); // send a speaking indicator

                var psi = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = $@"-i ""{sound.Filename}"" -ac 2 -f s16le -ar 48000 pipe:1",
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };
                var ffmpeg = Process.Start(psi);

                var output = ffmpeg.StandardOutput.BaseStream;
                var discord = connection.CreatePCMStream(AudioApplication.Voice);
                await output.CopyToAsync(discord);
                await discord.FlushAsync();

                await connection.SetSpeakingAsync(false); // we're not speaking anymore
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"- {ex.StackTrace}");
            }
        }


        private async Task OnVoiceStateUpdated(SocketUser user, SocketVoiceState state1, SocketVoiceState state2)
        {
            // Check if this was a non-bot user joining a voice channel
            if (user.IsBot)
                return;

            var guild = state2.VoiceChannel?.Guild ?? state1.VoiceChannel?.Guild;
            if (guild == null)
                return;

            var connection = _connections.GetValueOrDefault(guild.Id);
            if (state2.VoiceChannel == null && state1.VoiceChannel != null && connection != null)
            {
                // Disconnected
                if (!state1.VoiceChannel.Users.Any(u => !u.IsBot))
                {
                    await state1.VoiceChannel.DisconnectAsync();
                }
                return;
            }

            if (connection != null && connection.ConnectionState == ConnectionState.Connected)
            {
                // Already connected, someone else joined our channel, say hello
                await Task.Delay(1000);
                await Say(connection, ZapSound.Hello);
                return;
            }

            if (connection == null || connection.ConnectionState != ConnectionState.Connected)
            {
                ConnectToVoice(state2.VoiceChannel).Start();
            }
        }

        private async Task ConnectToVoice(SocketVoiceChannel voiceChannel)
        {
            if (voiceChannel == null)
                return;

            try
            {
                Console.WriteLine($"Connecting to channel {voiceChannel.Id}");
                var connection = await voiceChannel.ConnectAsync();
                Console.WriteLine($"Connected to channel {voiceChannel.Id}");
                _connections[voiceChannel.Guild.Id] = connection;

                await Task.Delay(1000);
                _lastZapped[voiceChannel.Guild.Id] = DateTime.Now;
                await Say(connection, ZapSound.Hello);
            }
            catch (Exception ex)
            {
                // Oh no, error
                Console.WriteLine(ex.Message);
                Console.WriteLine($"- {ex.StackTrace}");
            }
        }
    }
}
