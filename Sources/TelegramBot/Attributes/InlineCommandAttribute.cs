using System;

namespace TelegramBot.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InlineCommandAttribute : Attribute
    {
        public string Command { get; }

        public InlineCommandAttribute(string command)
        {
            Command = command;
        }
    }
}
