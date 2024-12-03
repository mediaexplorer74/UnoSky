using FishyFlip.Models;

namespace Birdsky.Core.Services.Bluesky;

public interface ISessionStorage
{
	Task<Session?> LoadSessionAsync();
	
	Task SaveSessionAsync(Session session);

	Task ClearSessionAsync();
}
