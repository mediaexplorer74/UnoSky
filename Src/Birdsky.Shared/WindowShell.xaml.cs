using Birdsky.Core.ViewModels;
using Birdsky.Infrastructure;
using MZikmund.Toolkit.WinUI.Infrastructure;
using Birdsky.Infrastructure;
using Windows.Foundation.Metadata;
using Birdsky.Services.Navigation;
using Birdsky.Core.Infrastructure;
using Birdsky.App;

namespace Birdsky;

public sealed partial class WindowShell : Page, IWindowShell
{
	private readonly IServiceScope _windowScope;
	private readonly Window _associatedWindow;
	private bool _isWindowClosed;

	public WindowShell(IServiceProvider serviceProvider, Window associatedWindow)
	{
		InitializeComponent();

		_windowScope = serviceProvider.CreateScope();
		var windowShellProvider = (WindowShellProvider)ServiceProvider.GetRequiredService<IWindowShellProvider>();
		windowShellProvider.SetShell(this, associatedWindow);
		ServiceProvider.GetRequiredService<INavigationService>().RegisterViewsFromAssembly(typeof(Birdsky.App.App).Assembly);

		ViewModel = ServiceProvider.GetRequiredService<WindowShellViewModel>();

		//_uiSettings.ColorValuesChanged += ColorValuesChanged;
		_associatedWindow = associatedWindow;
		_associatedWindow.Closed += OnWindowClosed;
		CustomizeWindow();

		Loading += WindowShell_Loading;
	}

	private void OnWindowClosed(object sender, WindowEventArgs args) => _isWindowClosed = true;

	private void WindowShell_Loading(FrameworkElement sender, object args)
	{
		((XamlRootProvider)ServiceProvider.GetRequiredService<IXamlRootProvider>()).XamlRoot = XamlRoot ?? throw new InvalidOperationException("XamlRoot must be set");
	}

	public IServiceProvider ServiceProvider => _windowScope.ServiceProvider;

	public WindowShellViewModel ViewModel { get; }

	public Frame RootFrame => InnerFrame;

	public bool HasCustomTitleBar { get; private set; }

	private void CustomizeWindow()
	{
		if (ApiInformation.IsPropertyPresent("Microsoft.UI.Xaml.Window", "ExtendsContentIntoTitleBar"))
		{
#if !HAS_UNO
			_associatedWindow.ExtendsContentIntoTitleBar = true;
			// TODO: The title bar grid will need to be resized along with TabBar
			_associatedWindow.SetTitleBar(TitleBarGrid);
			HasCustomTitleBar = true;
#endif
		}
		if (ApiInformation.IsPropertyPresent("Microsoft.UI.Xaml.Window", "SystemBackdrop"))
		{
			_associatedWindow.SystemBackdrop = new MicaBackdrop();
			Background = null;
		}
	}

	public void SetTitleBar(UIElement? titleBar)
	{
		if (!_isWindowClosed)
		{
			_associatedWindow.SetTitleBar(titleBar ?? TitleBarGrid);
		}
	}
}
