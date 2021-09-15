using System.Resources;
using Microsoft.Extensions.Localization;

// Change back from a file-scoped namespace to observe the intended behaviour
namespace FileScopedNamespacesResources;

public class Resources
{
    private readonly IStringLocalizer _localizer;

    static Resources()
    {
        var type = typeof(Resources);
        var assembly = type.Assembly;

        ResourceManager = new ResourceManager(type.FullName, assembly);
    }

    public Resources(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public static ResourceManager ResourceManager { get; private set; }

    public string TestString => _localizer["TestString"];
}
