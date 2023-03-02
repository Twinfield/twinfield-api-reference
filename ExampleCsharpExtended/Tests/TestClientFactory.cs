using System;
using TwinfieldApi.Services;

namespace Tests
{
	class TestClientFactory : IClientFactory
	{
		public ILoginSessionSoapClient CreateLoginSessionClient(string baseUrl)
		{
			return new TestLoginSessionSoapClient();
		}

		public ISessionSoapClient CreateSessionClient(string baseUrl)
		{
			return new TestSessionSoapClient();
		}

		public IBankBookServiceClient CreateBankBookClient(string baseUrl)
		{
			throw new NotImplementedException();
		}

		public IFinderSoapClient CreateFinderClient(string baseUrl)
		{
			return new TestFinderSoapClient();
		}

		public IProcessXmlSoapClient CreateProcessXmlClient(string baseUrl)
		{
			throw new NotImplementedException();
		}
	}
}