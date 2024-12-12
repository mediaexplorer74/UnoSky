using Birdsky.Core.ViewModels;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Microsoft.UI.Dispatching;

namespace Birdsky.Infrastructure;

public interface IWindowShell
{
	WindowShellViewModel ViewModel { get; }

	XamlRoot? XamlRoot { get; }

	IServiceProvider ServiceProvider { get; }

	DispatcherQueue DispatcherQueue { get; }

	Frame RootFrame { get; }

	void SetTitleBar(UIElement? titleBar);
}
