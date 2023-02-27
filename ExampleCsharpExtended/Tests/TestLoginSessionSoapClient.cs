using System;
using TwinfieldApi.Services;
using TwinfieldApi.TwinfieldLoginSessionService;

namespace Tests
{
	internal class TestLoginSessionSoapClient : ILoginSessionSoapClient
	{
		const string TestCluster = "https://c1.twinfield.com";
		const string TestUser = "TESTUSER";
		const string TestPassword = "password";
		const string TestOrganisation = "TESTORG";
		const string TestSessionId = "1234567890";

		public Header Logon(string user, string password, string organisation, out LogonResult LogonResult, out LogonAction nextAction,
				out string cluster)
		{
			if (LogonIsValid(user, password, organisation))
			{
				LogonResult = LogonResult.Ok;
				nextAction = LogonAction.None;
				cluster = TestCluster;
				return new Header { SessionID = TestSessionId };
			}

			LogonResult = LogonResult.Invalid;
			nextAction = LogonAction.None;
			cluster = string.Empty;
			return null;
		}

		public Header OAuthLogon(string clientToken, string clientSecret, string accessToken, string accessSecret,
				out LogonResult OAuthLogonResult, out LogonAction nextAction, out string cluster)
		{
			throw new NotImplementedException();
		}

		static bool LogonIsValid(string user, string password, string organisation)
		{
			return user == TestUser && password == TestPassword && organisation == TestOrganisation;
		}
	}
}