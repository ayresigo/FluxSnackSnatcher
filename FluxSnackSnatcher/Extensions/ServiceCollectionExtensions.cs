using Discord.Webhook;
using Firebase.Database;
using FluxSnackSnatcher.Facades;
using FluxSnackSnatcher.Services;
using FluxSnackSnatcher.Settings;

namespace FluxSnackSnatcher.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSingletons(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("ApiSettings").Get<ApiSettings>();

            services.AddSingleton(settings);

            services.AddSnatcherServices();
            services.AddDiscordServices(settings);
            services.AddFirebaseServices(settings);
        }

        public static void AddSnatcherServices(this IServiceCollection services)
        {
            services.AddScoped<ISnatcherFacade, SnatcherFacade>();
        }

        public static void AddDiscordServices(this IServiceCollection services, ApiSettings settings)
        {
            services.AddScoped(serviceProvider =>
            {
                return new DiscordWebhookClient(settings.DiscordWebhook.Id, settings.DiscordWebhook.Token);
            });

            services.AddScoped<IDiscordService, DiscordService>();
        }

        public static void AddFirebaseServices(this IServiceCollection services, ApiSettings settings)
        {
            services.AddScoped(serviceProvider =>
            {
                return new FirebaseClient(
                    settings.FireBase.BaseUrl,
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(settings.FireBase.ApiKey)
                    });
            });

            services.AddScoped<IFirebaseService, FirebaseService>();
        }
    }
}
