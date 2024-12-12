using FishyFlip.Models;
using System.Threading.Tasks;

namespace Birdsky.Core.Services.Bluesky;

public interface ISessionStorage
{
	Task<Session?> LoadSessionAsync();
	
	Task SaveSessionAsync(Session session);

	Task ClearSessionAsync();
}
