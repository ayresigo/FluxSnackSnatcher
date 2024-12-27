using FluxSnackSnatcher.Models;

namespace FluxSnackSnatcher.Facades
{
    public interface ISnatcherFacade
    {
        public Task<string> SnatchSnack(string? snacks, string? url, string? account);

        public Task<IDictionary<string, IList<CookieData>>> GetCookies();

        public Task AddAccount(AccountData account);
    }
}
