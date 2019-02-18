using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordZapBot
{
    public class ZapQuote
    {
        private static List<string> _quotes = new List<string>
        {
            "Anyways it was mostly kiffs fault.",
            "You see, Killbots have a preset kill limit. Knowing their weakness, I sent wave after wave of my own men at them, until they reached their limit and shut down.",
            "If I said you had a nice body, would you take your pants off and dance a little?",
            "Kiff I'm heading to the restroom, and I'll be needing an assistant.....Oh, sorry, you're crying like a woman.",
            "I'll be all over you like a fly on some very seductive manure.",
            "I am the man with no name. Zap Brannigan, at your service.",
            "Kif! Show them the medal I won.",
            "You're a man's man, you're a man's man's man!",
            "She's built like a steakhouse, but drives like a bistro!",
            "Brannigan's law is like Brannigan's love, hard and fast.",
            "When I'm on command every mission is a suicide mission.",
            "Where's the little umbrella Kif? That's what makes it a Scotch on the rocks!",
            "If we can hit that bull's-eye, the rest of the dominoes will fall like a house of cards. Checkmate!"
        };

        public static string RandomQuote()
        {
            return _quotes.Random();
        }
    }
}
