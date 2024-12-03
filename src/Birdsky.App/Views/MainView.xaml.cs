using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Birdsky.Core.ViewModels;
using Birdsky.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Birdsky.App.Views;

public sealed partial class MainView : MainViewBase
{
	public MainView()
	{
		this.InitializeComponent();
	}
}

public partial class MainViewBase : PageBase<MainViewModel> { }
