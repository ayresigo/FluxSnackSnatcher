namespace FluxSnackSnatcher.Settings
{
    public class ApiSettings
    {
        public int Port { get; set; }

        public int MinimumGroupId { get; set; }

        public FireBaseSettings FireBase { get; set; }

        public DiscordSettings DiscordWebhook { get; set; }
    }
}
