using System.Collections.ObjectModel;
using Birdsky.Core.Services.Bluesky;
using Birdsky.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.App.Bsky.Graph;

namespace Birdsky.Core.ViewModels;

public partial class MainViewModel : PageViewModel
{
	private readonly IBlueskyService _blueskyService;

	[ObservableProperty]
	private List<FeedViewPost> _feed;

	public MainViewModel(INavigationService navigationService, IBlueskyService blueskyService) : base(navigationService)
	{
		_blueskyService = blueskyService;
	}

	protected override async void OnActivated()
	{
		base.OnActivated();

		var timeline = await _blueskyService.GetTimelineAsync();
		if (timeline?.AsT0 is { } timelineData)
		{
			Feed = timelineData.Feed;
		}
	}
}
