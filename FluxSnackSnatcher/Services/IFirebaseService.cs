using FluxSnackSnatcher.Models;

namespace FluxSnackSnatcher.Services
{
    public interface IFirebaseService
    {
        public Task<string> SetCookie(CookieData cookie);

        public Task<List<CookieData>> GetCookies();
    }
}
