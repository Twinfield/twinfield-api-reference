using System;
using TwinfieldApi.Services;
using TwinfieldApi.TwinfieldSessionService;

namespace Tests
{
	internal class TestSessionSoapClient : ISessionSoapClient
	{
		public SMSLogonResult SmsLogon(Header Header, string smsCode, out LogonAction nextAction)
		{
			throw new NotImplementedException();
		}

		public int SmsSendCode(Header Header)
		{
			throw new NotImplementedException();
		}

		public ChangePasswordResult ChangePassword(Header Header, string currentPassword, string newPassword,
				out LogonAction nextAction)
		{
			throw new NotImplementedException();
		}

		public SelectCompanyResult SelectCompany(Header Header, string company)
		{
			return SelectCompanyResult.Ok;
		}

		public void KeepAlive(Header Header)
		{ }

		public void Abandon(Header Header)
		{ }

		public string GetRole(Header Header)
		{
			return "LVL1";
		}
	}
}