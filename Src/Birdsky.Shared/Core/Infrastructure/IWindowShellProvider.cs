//using Microsoft.UI.Dispatching;
using Birdsky.Infrastructure;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Birdsky.Core.Infrastructure;

public interface IWindowShellProvider
{
    Window Window { get; }

    IWindowShell Shell { get; }

    DispatcherQueue DispatcherQueue { get; }

    Frame RootFrame { get; }
}
