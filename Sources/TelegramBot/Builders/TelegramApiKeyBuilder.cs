namespace TelegramBot.Builders
{

    public class TelegramApiKeyBuilder
    {
        public string? ApiKey { get; set; }

        internal bool UseConfiguration { get; private set; }

        public void FromConfiguration()
        {
            UseConfiguration = true;
        }
    }
}