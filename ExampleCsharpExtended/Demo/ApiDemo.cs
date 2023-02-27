using System;
using System.Net;
using TwinfieldApi;
using TwinfieldApi.Services;

namespace Demo
{
	class ApiDemo
	{
		readonly IClientFactory clientFactory = new ClientFactory();
		Session session;

		public void Run(string loginServerUrl, string user, string password, string organization, string office)
		{
			if (!Login(loginServerUrl, user, password, organization))
				return;

			if (!SwitchToOffice(office))
				return;

			(new OrganizationDemo(session)).Run();
			(new DimensionDemo(session)).Run();
			(new BankBookDemo(session)).Run();
			(new BookkeepingDemo(session)).Run();

			LogOff();
		}

		bool Login(string loginServerUrl, string user, string password, string organization)
		{
			Console.WriteLine("Log in");
			var sessionService = new SessionService(clientFactory);
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
			session = sessionService.Logon(loginServerUrl, user, password, organization);
			if (session == null)
			{
				Console.WriteLine("Failed to log in to organization {0} with user {1} on {2}.", organization, user, loginServerUrl);
				return false;
			}

			Console.WriteLine("Logged in to organization {0} with user {1} on {2}.", organization, user, session.ClusterUrl);
			Console.WriteLine();
			return true;
		}

		bool SwitchToOffice(string office)
		{
			var sessionService = new SessionService(clientFactory);
			if (!sessionService.SelectOffice(session, office))
			{
				Console.WriteLine("Office {0} does not exist or you don't have sufficient rights to access it.", office);
				return false;
			}

			Console.WriteLine("Switched to office {0}.", office);
			Console.WriteLine();
			return true;
		}

		void LogOff()
		{
			var sessionService = new SessionService(clientFactory);
			sessionService.Abandon(session);

			Console.WriteLine("Logged off.");
			Console.WriteLine();
		}

	}
}