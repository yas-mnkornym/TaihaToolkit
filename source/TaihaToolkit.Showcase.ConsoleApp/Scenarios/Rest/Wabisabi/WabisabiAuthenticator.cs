using System;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Rest;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Rest.Wabisabi
{
	public class WabisabiAuthenticator : IRestAuthenticator
	{
		string ClientId { get; }
		string UserId { get; }
		string Password { get; }
		WabisabiAccessToken AccessToken { get; set; }
		
		public WabisabiAuthenticator(string userId, string password, string clientId)
		{
			if (userId == null) { throw new ArgumentNullException(nameof(userId)); }
			if (password == null) { throw new ArgumentNullException(nameof(password)); }
			if (clientId == null) { throw new ArgumentNullException(nameof(clientId)); }

			UserId = userId;
			Password = password;
			ClientId = clientId;
		}

		public async Task AuthenticateAsync(IRestClient client, IRestRequest request, IHeaderBag headerBag, IParameterBag parameterBag)
		{
			if (request is WabisabiLoginRequest) { return; }

			if (AccessToken?.IsValid != true) {
				var result = await client.RequestAsync(
					new WabisabiLoginRequest(),
					new WabisabiLoginRequestParameter {
						ClientId = ClientId,
						Username = UserId,
						Password = Password
					});
				AccessToken = result.SuccessResult;
			}

			if (AccessToken?.IsValid == true) {
				headerBag.Add("Authorization", string.Format("Bearer {0}", AccessToken.AccessToken));
			}
		}
	}
}
