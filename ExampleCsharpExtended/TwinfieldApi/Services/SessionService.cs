using TwinfieldApi.TwinfieldSessionService;

namespace TwinfieldApi.Services
{
	public class SessionService
	{
		const string DefaultLoginUrl = "https://login.twinfield.com";
		readonly IClientFactory clientFactory;

		public SessionService(IClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		public Session Logon(string user, string password, string organization)
		{
			return Logon(DefaultLoginUrl, user, password, organization);
		}

		public Session Logon(string loginUrl,  string user, string password, string organization)
		{
			var sessionClient = clientFactory.CreateLoginSessionClient(loginUrl);
			var sessionHeader = sessionClient.Logon(user, password, organization, out var logonResult, out var logonAction, out var clusterUrl);

			if (logonResult != TwinfieldLoginSessionService.LogonResult.Ok)
				return null;

			if (logonAction != TwinfieldLoginSessionService.LogonAction.None)
				return null;

			return new Session
			{
				SessionId = sessionHeader.SessionID,
				ClusterUrl = clusterUrl
			};
		}

		public bool SelectOffice(Session session, string office)
		{
			var sessionClient = clientFactory.CreateSessionClient(session.ClusterUrl);
			var result = sessionClient.SelectCompany(new Header { SessionID = session.SessionId }, office);
			if (result != SelectCompanyResult.Ok)
				return false;

			session.Office = office;
			return true;
		}

		public void Abandon(Session session)
		{
			if (!session.LoggedOn)
				return;

			var sessionClient = clientFactory.CreateSessionClient(session.ClusterUrl);
			sessionClient.Abandon(new Header { SessionID = session.SessionId });

			session.Clear();
		}
	}
}
