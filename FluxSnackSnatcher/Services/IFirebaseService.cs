using FluxSnackSnatcher.Models;

namespace FluxSnackSnatcher.Services
{
    public interface IFirebaseService
    {
        public Task<string> SetCookie(CookieData cookie);

        public Task<IDictionary<string, IList<CookieData>>> GetCookies();

        Task<string> SetAccount(AccountData account);
    }
}
