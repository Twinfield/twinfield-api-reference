using TwinfieldApi.Services;
using TwinfieldApi.TwinfieldFinderService;

namespace Tests
{
	class TestFinderSoapClient : IFinderSoapClient
	{
		public MessageOfErrorCodes[] Search(Header Header, string type, string pattern, int field, int firstRow, int maxRows,
				string[][] options, out FinderData data)
		{
			data = new FinderData();
			//data.Items = new string[][] { };
			return new MessageOfErrorCodes[] { };
		}
	}
}