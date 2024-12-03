using Birdsky.Core.Infrastructure;
using Birdsky.Core.Services.Bluesky;
using Birdsky.Core.ViewModels;
using Birdsky.Infrastructure;
using Birdsky.Services.Navigation;
using CommunityToolkit.Mvvm.DependencyInjection;
using MZikmund.Toolkit.WinUI.Infrastructure;
using System.Data.Common;
using Uno.Resizetizer;

namespace Birdsky.App;

public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected Window? MainWindow { get; private set; }
    internal static IHost? Host { get; private set; }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = this.CreateBuilder(args)
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                        .Section<AppConfig>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                .ConfigureServices((context, services) => ConfigureServices(services))
            );
        MainWindow = builder.Window;
#if DEBUG
        MainWindow.EnableHotReload();
#endif

        Host = builder.Build();
        Ioc.Default.ConfigureServices(Host.Services);

        // Do not repeat app initialization when the Window already has content,
        // just ensure that the window is active
        if (MainWindow.Content is not WindowShell windowShell)
        {
            // Create a Frame to act as the navigation context and navigate to the first page
            windowShell = new WindowShell(Host.Services, MainWindow);

            // Place the frame in the current Window
            MainWindow.Content = windowShell;
        }

        if (windowShell.RootFrame.Content is null)
        {
            // When the navigation stack isn't restored navigate to the first page,
            // configuring the new page by passing required information as a navigation
            // parameter
            windowShell.ServiceProvider.GetRequiredService<INavigationService>().Navigate<LoginViewModel>(args.Arguments);
        }

        // Ensure the current window is active
        MainWindow.Activate();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<WindowShellViewModel>();
        services.AddScoped<LoginViewModel>();
        services.AddScoped<MainViewModel>();

		services.AddScoped<IFrameProvider, FrameProvider>();
        services.AddScoped<INavigationService, NavigationService>();
        services.AddScoped<IWindowShellProvider, WindowShellProvider>();
        services.AddScoped<IBlueskyService, BlueskyService>();
		services.AddSingleton<ICredentialsService, CredentialsService>();

		services.AddScoped<IXamlRootProvider, XamlRootProvider>();
    }
}
