using FluxSnackSnatcher.Models;

namespace FluxSnackSnatcher.Facades
{
    public interface ISnatcherFacade
    {
        public Task<string> SnatchSnack(string? snacks, string url);

        public Task<IDictionary<string, IList<CookieData>>> GetCookies();
    }
}
