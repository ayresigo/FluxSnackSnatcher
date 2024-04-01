namespace FluxSnackSnatcher.Services
{
    public interface IDiscordService
    {
        public Task SendMessage(string message);
    }
}
