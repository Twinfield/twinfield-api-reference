using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwinfieldApi.Services;

namespace Tests
{
	[TestClass]
	public class SessionServiceTests
	{
		SessionService sessionService;

		[TestInitialize]
		public void Initialize()
		{
			var factory = new TestClientFactory();
			sessionService = new SessionService(factory);
		}

		[TestMethod]
		public void Logon_with_valid_credentials_should_create_session()
		{
			var session = sessionService.Logon("TESTUSER", "password", "TESTORG");

			Assert.IsNotNull(session);
			Assert.IsTrue(session.LoggedOn);
		}

		[TestMethod]
		public void Logon_with_invalid_credentials_should_not_create_session()
		{
			var session = sessionService.Logon("TESTUSER", "PASSWORD", "TESTORG");

			Assert.IsNull(session);
		}

		[TestMethod]
		public void Selecting_an_office_should_set_the_sessions_office()
		{
			var session = Utilities.CreateValidSession();
			sessionService.SelectOffice(session, "002");

			Assert.AreEqual("002", session.Office);
		}

		[TestMethod]
		public void Abandoning_a_session_should_clear_it()
		{
			var session = Utilities.CreateValidSession();
			sessionService.Abandon(session);

			Assert.IsFalse(session.LoggedOn);
		}

	}
}
