using System;
using System.Linq;
using TwinfieldApi.TwinfieldFinderService;

namespace TwinfieldApi.Services
{
	class FinderService
	{
		readonly Session session;
		readonly IClientFactory clientFactory;

		public FinderService(Session session)
			: this(session, new ClientFactory())
		{ }

		public FinderService(Session session, IClientFactory clientFactory)
		{
			this.session = session;
			this.clientFactory = clientFactory;
		}		 

		public FinderData Search(Query query)
		{
			var client = clientFactory.CreateFinderClient(session.ClusterUrl);
			var header = new Header { SessionID = session.SessionId };

			var messages = client.Search(header, query.Type, query.Pattern,
				query.Field, query.FirstRow, query.MaxRows, query.Options, out var data);
			AssertNoMessages(messages);

			return data;
		}

		static void AssertNoMessages(MessageOfErrorCodes[] messages)
		{
			if (messages.Any())
				throw new FinderException(messages);
		}

		public class Query
		{
			public string @Type { get; set; }
			public string Pattern { get; set; }
			public int Field { get; set; }
			public int FirstRow { get; set; }
			public int MaxRows { get; set; }
			public string[][] Options { get; set; }

			public Query()
			{
				Field = 0;
				FirstRow = 1;
				MaxRows = 10;
				Options = null;
			}
		}
	}

	public class FinderException : Exception
	{
		public MessageOfErrorCodes[] Messages { get; }

		public FinderException(MessageOfErrorCodes[] messages)
		{
			Messages = messages;
		}

		public override string Message
		{
			get
			{
				var messages = Messages.Select(m => m.Text).ToArray();
				return string.Join(". ", messages);
			}
		}
	}

}