using FishyFlip;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Models;

namespace Birdsky.Core.Services.Bluesky;

public class BlueskyService : IBlueskyService
{
	private const string ResourceName = "BlueskyClientSession";
	private readonly ICredentialsService _credentialsService;
	private ATProtocol _client;
	private Session? _session;

	public BlueskyService(ICredentialsService credentialsService)
	{
		_client = new ATProtocolBuilder().EnableAutoRenewSession(true).Build();
		_credentialsService = credentialsService;
	}

	public bool IsLoggedIn => _client.Session != null;


	public async Task<bool> InitializeAsync()
	{
		var credentials = _credentialsService.RetrieveCredentials();
		if (credentials.handle is not null && credentials.appPassword is not null)
		{
			_session = await _client.AuthenticateWithPasswordAsync(credentials.handle, credentials.appPassword);
			return _session is not null;
		}
		return false;
	}

	public async Task<bool> LoginAsync(string handle, string appPassword)
	{
		_session = await _client.AuthenticateWithPasswordAsync(handle, appPassword);
		if (_session is not null)
		{
			_credentialsService.SaveCredentials(handle, appPassword);
		}

		return _session is not null;
	}

	public async Task<Result<GetTimelineOutput?>> GetTimelineAsync() => await _client.GetTimelineAsync();
}
