using TwinfieldApi.TwinfieldBankBookService;
using TwinfieldApi.TwinfieldFinderService;
using TwinfieldApi.TwinfieldSessionService;
using Header = TwinfieldApi.TwinfieldLoginSessionService.Header;
using LogonAction = TwinfieldApi.TwinfieldLoginSessionService.LogonAction;
using LogonResult = TwinfieldApi.TwinfieldLoginSessionService.LogonResult;

namespace TwinfieldApi.Services
{
	public interface ILoginSessionSoapClient
	{
		Header Logon(string user, string password, string organisation, out LogonResult LogonResult, out LogonAction nextAction, out string cluster);
		Header OAuthLogon(string clientToken, string clientSecret, string accessToken, string accessSecret, out LogonResult OAuthLogonResult, out LogonAction nextAction, out string cluster);
	}

	public interface ISessionSoapClient
	{
		SMSLogonResult SmsLogon(TwinfieldSessionService.Header Header, string smsCode, out TwinfieldSessionService.LogonAction nextAction);
		int SmsSendCode(TwinfieldSessionService.Header Header);
		ChangePasswordResult ChangePassword(TwinfieldSessionService.Header Header, string currentPassword, string newPassword, out TwinfieldSessionService.LogonAction nextAction);
		SelectCompanyResult SelectCompany(TwinfieldSessionService.Header Header, string company);
		void KeepAlive(TwinfieldSessionService.Header Header);
		void Abandon(TwinfieldSessionService.Header Header);
		string GetRole(TwinfieldSessionService.Header Header);
	}

	public interface IBankBookServiceClient
	{
		void Process(string SessionId, Command Command);
		QueryResult Query(string SessionId, Query Query1);
	}

	public interface IFinderSoapClient
	{
		MessageOfErrorCodes[] Search(TwinfieldFinderService.Header Header, string type, string pattern, int field, int firstRow, int maxRows, string[][] options, out FinderData data);
	}

	public interface IProcessXmlSoapClient
	{
		string ProcessXmlString(TwinfieldProcessXmlService.Header Header, string xmlRequest);
		System.Xml.XmlNode ProcessXmlDocument(TwinfieldProcessXmlService.Header Header, System.Xml.XmlNode xmlRequest);
		byte[] ProcessXmlCompressed(TwinfieldProcessXmlService.Header Header, byte[] xmlRequest);
	}
}
