namespace TelegramBot.Builders
{
    public class TelegramServerBuilder
    {
        public string? BaseUrl { get; set; }

        internal bool UseConfiguration { get; private set; }

        public void FromConfiguration()
        {
            UseConfiguration = true;
        }
    }
}