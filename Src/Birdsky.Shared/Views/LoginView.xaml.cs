using Birdsky.Core.ViewModels;
using Birdsky.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Birdsky.App.Views;

public sealed partial class LoginView : LoginViewBase
{
	public LoginView()
	{
		this.InitializeComponent();
	}

	private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
	{
		ViewModel!.AppPassword = ((PasswordBox)sender).Password;
	}
}

public partial class LoginViewBase : PageBase<LoginViewModel> { }
