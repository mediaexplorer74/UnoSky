using Birdsky;
using Birdsky.Core.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Birdsky.Views;

public abstract partial class PageBase<TViewModel> : Page
    where TViewModel : PageViewModel
{
    private object? _pendingParameter;
    private bool _isNavigationDelayed;

    protected PageBase()
    {
        Loading += PageLoading;
        Loaded += PageLoaded;
        Unloaded += PageUnloaded;
    }

    public virtual TViewModel? ViewModel { get; private set; }

    private void PageLoading(object sender, object args)
    {
        EnsureViewModel();

        if (_isNavigationDelayed)
        {
            ViewModel.ViewNavigatedToInternal(_pendingParameter);
            _pendingParameter = null;
        }

        ViewModel?.ViewLoading();
    }

    private void PageLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel?.ViewLoaded();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        EnsureViewModel();

        if (ViewModel is not null)
        {
            ViewModel.ViewNavigatedToInternal(_pendingParameter ?? e.Parameter);
        }
        else
        {
            _isNavigationDelayed = true;
            _pendingParameter = e.Parameter;
        }
    }

    private void PageUnloaded(object sender, RoutedEventArgs e)
    {
        ViewModel?.ViewUnloaded();
    }

    private void EnsureViewModel()
    {
        if (ViewModel is not null)
        {
            return;
        }

        //TODO
        if (1==1)//(XamlRoot?.Content is WindowShell windowShell)
        {
            ViewModel = default;//windowShell.ServiceProvider.GetRequiredService<TViewModel>();
			DataContext = ViewModel;
        }
    }
}
