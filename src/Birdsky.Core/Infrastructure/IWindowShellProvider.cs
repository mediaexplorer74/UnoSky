using Microsoft.UI.Dispatching;
using Birdsky.Infrastructure;

namespace Birdsky.Core.Infrastructure;

public interface IWindowShellProvider
{
    Window Window { get; }

    IWindowShell Shell { get; }

    DispatcherQueue DispatcherQueue { get; }

    Frame RootFrame { get; }
}
