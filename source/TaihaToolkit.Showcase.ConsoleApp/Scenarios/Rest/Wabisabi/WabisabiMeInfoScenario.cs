using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Codeplex.Data;
using Studiotaiha.Toolkit.ConsoleUtils;
using Studiotaiha.Toolkit.Rest;
using Studiotaiha.Toolkit.Rest.Clients;
using Studiotaiha.Toolkit.Rest.Requests;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Rest.Wabisabi
{
    class WabisabiMeInfoScenario : RestScenarioBase
    {
        public static string WabisabiUrl = "https://comihq.studio-taiha.net";

        public WabisabiMeInfoScenario() 
            : base("Me info on ComiHQ Web", "Get user info on ComiHQ Web using Rest client library", true)
        {
        }

		public override void Execute()
		{
			Console.Write("User ID:");
			var userId = Console.ReadLine().Trim();

			Console.Write("Password:");
			var password = Console.ReadLine().Trim();

			Console.Write("Client ID:");
			var clientId = Console.ReadLine().Trim();

			var client = new RestClient(new Uri(WabisabiUrl)) {
				Authenticator = new WabisabiAuthenticator(userId, password, clientId)
			};
			Console.WriteLine("Requesting Me info...");
			try {
				var ret = client.RequestAsync(new DelegateRequest<object, dynamic, object>(HttpMethod.Get, "api/user/me") {
					ParseSuccessResultAsyncHandler = async (statusCode, requestResult) => {
						return DynamicJson.Parse(await requestResult.ReadAsStringAsync());
					},
				}, null).Result.SuccessResult;

				Console.WriteLine("Guid: {0}", ret.Guid);
				Console.WriteLine("IdName: {0}", ret.IdName);
				Console.WriteLine("DisplayName: {0}", ret.DisplayName);
				Console.WriteLine("MailAddress: {0}", ret.MailAddress);
				Console.WriteLine("PhoneNumber: {0}", ret.PhoneNumber);
			}
			catch(AggregateException aex) {
				using (new ForegroundColorScope(ConsoleColor.Red)) {
					Console.WriteLine("Failed to request me info.");

					var ex = aex.InnerExceptions.FirstOrDefault(x => x is RestRequestFailureException) as RestRequestFailureException;
					if (ex != null) {
						Console.WriteLine("StatusCode: {0}", ex.StatusCode);
						Console.WriteLine("[Response Body]");
						Console.WriteLine(ex.ResponseBody);
						Console.WriteLine();
						Console.WriteLine("[Exception]");
						Console.WriteLine(ex.Message);
					}
					else {
						Console.WriteLine("[Exception]");
						Console.WriteLine(aex);
					}
				}
			}
		}
    }
}
