using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class LocalizatorExtensions
{
    public static void AddLocalizator<T>(this IServiceCollection services, IConfiguration configuration)
        where T: ILocalizationBook
    {
        if (configuration == null)
        {
            throw new ArgumentNullException();
        }

        services.Configure<LocalizatorOptions<T>>(configuration.GetSection("LocalizatorOptions"));
        services.AddSingleton<Localizator<T>>();
    }

    public static void AddLocalizator<T>(this IServiceCollection services, Action<LocalizatorOptions<T>> options)
        where T: ILocalizationBook
    {
        if (options == null)
        {
            throw new ArgumentNullException();
        }
        
        services.Configure(options);
        services.AddSingleton<Localizator<T>>();
    }
}