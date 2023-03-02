using TwinfieldApi.Services;
using TwinfieldApi.TwinfieldBankBookService;

namespace TwinfieldApi.Bankbooks
{
	public class BankbookService
	{
		readonly Session session;
		readonly IClientFactory clientFactory;

		public BankbookService(Session session)
			: this(session, new ClientFactory())
		{}

		public BankbookService(Session session, IClientFactory clientFactory)
		{
			this.session = session;
			this.clientFactory = clientFactory;
		}

		public Bankbook FindBankBook(string code)
		{
			var queryResult = Query(new GetBankBook { Code = code });
			return Bankbook.FromQueryResult(code, queryResult);
		}

		QueryResult Query(GetBankBook query)
		{
			var bankBookClient = clientFactory.CreateBankBookClient(session.ClusterUrl);
			return bankBookClient.Query(session.SessionId, query);
		}
	}
}
