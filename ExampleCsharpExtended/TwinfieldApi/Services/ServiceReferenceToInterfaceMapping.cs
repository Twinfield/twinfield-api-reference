using TwinfieldApi.Services;

namespace TwinfieldApi.TwinfieldLoginSessionService
{
	public partial class SessionSoapClient : ILoginSessionSoapClient
	{ }
}

namespace TwinfieldApi.TwinfieldSessionService
{
	public partial class SessionSoapClient : ISessionSoapClient
	{ }
}

namespace TwinfieldApi.TwinfieldBankBookService
{
	public partial class BankBookServiceClient : IBankBookServiceClient
	{ }
}

namespace TwinfieldApi.TwinfieldFinderService
{
	public partial class FinderSoapClient : IFinderSoapClient
	{ }
}

namespace TwinfieldApi.TwinfieldProcessXmlService
{
	public partial class ProcessXmlSoapClient : IProcessXmlSoapClient
	{ }
}