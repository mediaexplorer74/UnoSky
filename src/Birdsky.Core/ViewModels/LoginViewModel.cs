using System.Reflection.Metadata;
using Birdsky.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishyFlip;
using FishyFlip.Models;

namespace Birdsky.Core.ViewModels;

public partial class LoginViewModel : PageViewModel
{
	private readonly INavigationService _navigationService;

	[ObservableProperty]
	private string handle;

	[ObservableProperty]
	private string appPassword;

	public LoginViewModel(INavigationService navigationService) : base(navigationService)
    {
		_navigationService = navigationService;
	}

	[RelayCommand]
    private async Task LoginAsync()
    {
        try
        {
            var client = new ATProtocolBuilder().EnableAutoRenewSession(true).Build();
            var session = await client.AuthenticateWithPasswordAsync(Handle, AppPassword);

            // Navigate to PostsView
            _navigationService.Navigate<MainViewModel>(client);
        }
        catch (Exception ex)
        {
            //// Handle login error
            //var dialog = new ContentDialog
            //{
            //    Title = "Login Failed",
            //    Content = ex.Message,
            //    CloseButtonText = "OK",
            //    XamlRoot = App.MainWindow.Content.XamlRoot
            //};
            //await dialog.ShowAsync();
        }
    }
}
