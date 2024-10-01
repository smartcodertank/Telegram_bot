using System;
using System.Text.RegularExpressions;

namespace TelegramBot.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TextQueryAttribute : Attribute
    {
        public Regex? Regex { get; }

        public TextQueryAttribute(string? pattern = null)
        {
            if (pattern != null)
            {
                Regex = new Regex(pattern);
            }
        }
    }
}