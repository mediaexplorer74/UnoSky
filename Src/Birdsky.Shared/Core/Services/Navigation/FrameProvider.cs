using Birdsky.Core.Infrastructure;
using Windows.UI.Xaml.Controls;

namespace Birdsky.Services.Navigation;

public class FrameProvider : IFrameProvider
{
	private readonly IWindowShellProvider _windowShellProvider;

	public FrameProvider(IWindowShellProvider windowShellProvider)
	{
		_windowShellProvider = windowShellProvider;
	}

    //public Frame GetForCurrentView()
    //{
    //    return _windowShellProvider.RootFrame;
    //}

    public Frame GetForCurrentView()
    {
        return _windowShellProvider.RootFrame;
    }

    
}
