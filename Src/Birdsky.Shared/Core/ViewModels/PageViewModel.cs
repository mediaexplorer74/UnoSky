using Birdsky.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Birdsky.Core.ViewModels;

public abstract partial class PageViewModel : ObservableRecipient
{
	protected PageViewModel(INavigationService navigationService)
	{
		NavigationService = navigationService ?? 
			throw new ArgumentNullException(nameof(navigationService));
	}

	public INavigationService NavigationService { get; }

	public bool CanGoBack => NavigationService.CanGoBack;

    //[RelayCommand]
    public virtual void GoBack()
    {
        NavigationService.GoBack();
    }

    //[ObservableProperty]
    private string _title = "";

	//[ObservableProperty]
	private bool _isWorking;

	public virtual void ViewCreated() { }

	public virtual void ViewLoading() { }

	public virtual void ViewLoaded() { }

	public virtual void ViewUnloaded() { }

	public void ViewNavigatedToInternal(object? parameter)
	{
		OnPropertyChanged(nameof(CanGoBack));
		OnNavigatedTo(parameter);
	}

	public virtual void OnNavigatedTo(object? parameter) { }
}
