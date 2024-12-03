using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Models;

namespace Birdsky.Core.Services.Bluesky;

public interface IBlueskyService
{
	bool IsLoggedIn { get; }

	Task<bool> InitializeAsync();

	Task<bool> LoginAsync(string handle, string appPassword);

	Task<Result<GetTimelineOutput?>> GetTimelineAsync();
}
