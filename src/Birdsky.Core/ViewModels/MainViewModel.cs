using Birdsky.Core.Services.Bluesky;
using Birdsky.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishyFlip.Lexicon.App.Bsky.Feed;

namespace Birdsky.Core.ViewModels;

public partial class MainViewModel : PageViewModel
{
	private readonly IBlueskyService _blueskyService;

	public MainViewModel(INavigationService navigationService, IBlueskyService blueskyService) : base(navigationService)
	{
		_blueskyService = blueskyService;
	}

	[ObservableProperty]
	private List<FeedItemViewModel> _feed;

	[ObservableProperty]
	private string _text;

	[RelayCommand]
	public async Task RefreshAsync()
	{
		var timeline = await _blueskyService.GetTimelineAsync();
		if (timeline?.AsT0 is { Feed: { } } timelineData)
		{
			Feed = timelineData.Feed
				.Select(f => f.Post)
				.Where(p => p is not null)
				.Select(post => new FeedItemViewModel(
					post!.Author?.DisplayName ?? "",
					(post.Record as Post)?.Text ?? "",
					post.LikeCount ?? 0,
					post.RepostCount ?? 0,
					post.ReplyCount ?? 0)).ToList();
		}
	}

	[RelayCommand]
	public async Task PostAsync()
	{
		await _blueskyService.PostAsync(Text);
		await RefreshAsync();
		Text = "";
	}

	public override async void OnNavigatedTo(object? parameter)
	{
		base.OnNavigatedTo(parameter);

		await RefreshAsync();
	}
}
