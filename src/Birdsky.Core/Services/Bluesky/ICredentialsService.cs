namespace Birdsky.Core.Services.Bluesky;

public interface ICredentialsService
{
	void SaveCredentials(string handle, string appPassword);

	(string? handle, string? appPassword) RetrieveCredentials();
}
