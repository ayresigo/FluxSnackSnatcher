
using Discord.Webhook;

namespace FluxSnackSnatcher.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly DiscordWebhookClient _discordWebhookClient;

        public DiscordService(DiscordWebhookClient discordWebhookClient)
        {
            _discordWebhookClient = discordWebhookClient;
        }

        public async Task SendMessage(string message)
        {
            await _discordWebhookClient.SendMessageAsync(message);
        }
    }
}
