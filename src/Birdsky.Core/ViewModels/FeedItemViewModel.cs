namespace Birdsky.Core.ViewModels;

public record class FeedItemViewModel(string Author, string Text, long Likes, long Reposts, long Replies);
