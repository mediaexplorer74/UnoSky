using Birdsky.Core.Services.Bluesky;
using Birdsky.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishyFlip.Lexicon.Com.Atproto.Server;
using GalaSoft.MvvmLight.Command;
using System;

//using System.Reflection.Metadata;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
//using MZikmund.Toolkit.WinUI.Infrastructure;

namespace Birdsky.Core.ViewModels;

public partial class LoginViewModel : PageViewModel
{
    private string Handle;
    internal string AppPassword;
    private readonly INavigationService _navigationService;
	private readonly IBlueskyService _blueskyService;
    

    //private readonly IXamlRootProvider _xamlRootProvider;

    //[ObservableProperty]
    private string _handle;

	//[ObservableProperty]
	private string _appPassword;

	public LoginViewModel(INavigationService navigationService, IBlueskyService blueskyService 
		/*, IXamlRootProvider xamlRootProvider*/) : base(navigationService)
	{
		_navigationService = navigationService;
		_blueskyService = blueskyService;
		//_xamlRootProvider = xamlRootProvider;
	}

	public override async void OnNavigatedTo(object? parameter)
	{
		base.OnNavigatedTo(parameter);
		await _blueskyService.InitializeAsync();

		if (_blueskyService.IsLoggedIn)
		{
			_navigationService.Navigate<MainViewModel>();
		}
	}

	//[RelayCommand]
	private async Task LoginAsync()
	{
		bool succeeded = false;
		try
		{
			if (await _blueskyService.LoginAsync(Handle, AppPassword))
			{
				succeeded = true;
				_navigationService.Navigate<MainViewModel>();
			}
			else
			{
				succeeded = false;
			}
		}
		catch (Exception)
		{
			succeeded = false;
		}
		finally
		{
			if (!succeeded)
			{
				// Handle login error
				var dialog = new ContentDialog
				{
					Title = "Login Failed",
					Content = "Could not log in, please try again.",
					CloseButtonText = "OK",
					//XamlRoot = _xamlRootProvider.XamlRoot,
				};

				await dialog.ShowAsync();
			}
		}
	}
}
