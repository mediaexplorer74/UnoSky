
using Windows.Foundation.Metadata;
using Windows.Security.Credentials;
using Windows.Storage;

namespace Birdsky.Core.Services.Bluesky;

public class CredentialsService : ICredentialsService
{
	private const string AppName = "Birdsky";

	private readonly PasswordVault _passwordVault;
	private readonly ApplicationDataContainer _localSettings 
		= ApplicationData.Current.LocalSettings;

	public CredentialsService()
	{
		if (ApiInformation.IsMethodPresent(typeof(PasswordCredential).FullName, "Retrieve"))
		{
			_passwordVault = new PasswordVault();
		}
	}

	public (string? handle, string? appPassword) RetrieveCredentials()
	{
		string? appPassword = null;
		if (_localSettings.Values.TryGetValue("UserHandle", out var value) && value is string handle)
		{
			if (_passwordVault is not null)
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
		if (_passwordVault is not null)
		{
			_passwordVault.Add(new PasswordCredential(AppName, handle, appPassword));
		}
		else
		{
			_localSettings.Values["UserPassword"] = appPassword;
		}
	}
}
