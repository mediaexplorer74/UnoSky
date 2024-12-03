
using Windows.Foundation.Metadata;
using Windows.Security.Credentials;

namespace Birdsky.Core.Services.Bluesky;

public class CredentialsService : ICredentialsService
{
	private const string AppName = "Birdsky";

	private readonly PasswordVault _passwordVault = new();
	private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

	public (string? handle, string? appPassword) RetrieveCredentials()
	{
		string? appPassword = null;
		if (_localSettings.Values.TryGetValue("UserHandle", out var value) && value is string handle)
		{
			if (ApiInformation.IsTypePresent(typeof(PasswordCredential).FullName))
			{
				var credential = _passwordVault.Retrieve(AppName, handle);
				appPassword = credential.Password;
				return (handle, appPassword);
			}
			else if (_localSettings.Values.TryGetValue("UserPassword", out var password) && password is string userPassword)
			{
				appPassword = userPassword;
				return (handle, appPassword);
			}
		}

		return (null, null);
	}

	public void SaveCredentials(string handle, string appPassword)
	{
		_localSettings.Values["UserHandle"] = handle;
		if (ApiInformation.IsTypePresent(typeof(PasswordCredential).FullName))
		{
			_passwordVault.Add(new PasswordCredential(AppName, handle, appPassword));
		}
		else
		{
			_localSettings.Values["UserPassword"] = appPassword;
		}
	}
}
