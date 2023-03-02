namespace TwinfieldApi.Services
{
	public interface IClientFactory
	{
		ILoginSessionSoapClient CreateLoginSessionClient(string baseUrl);
		ISessionSoapClient CreateSessionClient(string baseUrl);
		IBankBookServiceClient CreateBankBookClient(string baseUrl);
		IFinderSoapClient CreateFinderClient(string baseUrl);
		IProcessXmlSoapClient CreateProcessXmlClient(string baseUrl);
	}
}