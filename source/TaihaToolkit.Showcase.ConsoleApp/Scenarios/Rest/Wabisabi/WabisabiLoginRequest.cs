using System;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Rest;
using Studiotaiha.Toolkit.Rest.Requests;
using Studiotaiha.Toolkit.Rest.ResultParsers;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Rest.Wabisabi
{
	[ResultParser(typeof(DataContractJsonSerializerResultParser), true, false)]
    class WabisabiLoginRequest : NoFaiulreResultPostRequest<WabisabiLoginRequestParameter, WabisabiAccessToken>
    {
        public WabisabiLoginRequest()
            : base("api/auth/token")
        { }

        public override Task ConfigureParameterAsync(IParameterBag parameterBag, WabisabiLoginRequestParameter parameter)
        {
            parameterBag.Body.Add("grant_type", "password");
            parameterBag.Body.Add("client_id", parameter.ClientId);
            parameterBag.Body.Add("username", parameter.Username);
            parameterBag.Body.Add("password", parameter.Password);

            return Task.FromResult(0);
        }

        protected override Task<WabisabiAccessToken> PostProcessSuccessResultAsync(WabisabiAccessToken result, HttpStatusCode httpStatusCode, IRequestResult requestResult)
        {
            result.ExpiresAt = DateTimeOffset.Now.AddSeconds(result.ExpiresIn);
            return Task.FromResult(result);
        }
    }

    class WabisabiLoginRequestParameter
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ClientId { get; set; }
    }
    
    [DataContract]
    class WabisabiAccessToken
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "expires_in")]
        public long ExpiresIn { get; set; }
        
        [DataMember]
        public DateTimeOffset ExpiresAt { get; set; }

        [IgnoreDataMember]
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(AccessToken) && (ExpiresAt > DateTimeOffset.Now || !string.IsNullOrWhiteSpace(RefreshToken));
            }
        }
    }
}
