using System.Globalization;
using System.Resources;
using FileScopedNamespacesResources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

[assembly: NeutralResourcesLanguage("en")]

var services = new ServiceCollection()
    .AddLogging(builder => builder.AddConsole())
    .AddLocalization(options => options.ResourcesPath = nameof(Resources))
    .AddSingleton(serviceProvider =>
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var localizer = new ResourceManagerStringLocalizer(
            Resources.ResourceManager,
            typeof(Resources).Assembly,
            typeof(Resources).Name,
            new ResourceNamesCache(),
            loggerFactory.CreateLogger<ResourceManagerStringLocalizer>());

        return new Resources(localizer);
    });

var serviceProvider = services.BuildServiceProvider();

var resources = serviceProvider.GetRequiredService<Resources>();

foreach (var culture in new[] { "en", "en-AU", "en-GB", "en-IE", "en-NZ", "en-US", "it-IT" })
{
    using (CultureSwitcher.WithCulture(culture))
    {
        Console.WriteLine($"{nameof(Resources)}:{culture} = {resources.TestString}");
    }
}

public sealed class CultureSwitcher : IDisposable
{
    private readonly CultureInfo _originalCulture;

    public CultureSwitcher(string name)
    {
        var culture = CultureInfo.GetCultureInfo(name);
        _originalCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
    }

    public static IDisposable WithCulture(string name)
        => new CultureSwitcher(name);

    public void Dispose()
    {
        CultureInfo.CurrentCulture = _originalCulture;
        CultureInfo.CurrentUICulture = _originalCulture;
    }
}
