using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordZapBot
{
    public static class RandomExtensions
    {
        public static T Random<T>(this IList<T> items)
        {
            var random = new Random();

            return items[random.Next(items.Count)];
        }
    }
}
