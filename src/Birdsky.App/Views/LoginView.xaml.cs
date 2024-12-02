using Birdsky.Core.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Birdsky.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Birdsky.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
}
