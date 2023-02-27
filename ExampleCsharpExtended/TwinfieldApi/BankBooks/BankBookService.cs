using TwinfieldApi.Services;
using TwinfieldApi.TwinfieldBankBookService;

namespace TwinfieldApi.BankBooks
{
	public class BankBookService
	{
		readonly Session session;
		readonly IClientFactory clientFactory;

		public BankBookService(Session session)
			: this(session, new ClientFactory())
		{}

		public BankBookService(Session session, IClientFactory clientFactory)
		{
			this.session = session;
			this.clientFactory = clientFactory;
		}

		public BankBook FindBankBook(string code)
		{
			var queryResult = Query(new GetBankBook { Code = code });
			return BankBook.FromQueryResult(code, queryResult);
		}

		QueryResult Query(GetBankBook query)
		{
			var bankBookClient = clientFactory.CreateBankBookClient(session.ClusterUrl);
			return bankBookClient.Query(session.SessionId, query);
		}
	}
}
