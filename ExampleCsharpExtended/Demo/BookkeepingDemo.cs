using System;
using System.Linq;
using TwinfieldApi;
using TwinfieldApi.Bookkeeping;
using TwinfieldApi.Browse;
using TwinfieldApi.Browse.Data;
using TwinfieldApi.Browse.Query;

namespace Demo
{
	class BookkeepingDemo
	{
		readonly Session session;
		readonly BookkeepingService bookkeepingService;

		public BookkeepingDemo(Session session)
		{
			this.session = session;
			bookkeepingService = new BookkeepingService(session);
		}

		public void Run()
		{
			BrowseTransactions();
			//TODO: Use browse results as input to read an existing transaction
			//ReadTransaction();
		}

		void BrowseTransactions()
		{
			const string transactionListBrowseCode = "020";

			Console.WriteLine("Browse transactions list");

			var browseService = new BrowseService(session);
			var browseDefinition = browseService.ReadBrowseDefinition(transactionListBrowseCode);
			var browseQuery = CreateBrowseQuery(browseDefinition);

			var transactionList = browseService.Read(browseQuery);
			DisplayBrowseResult(transactionList);

			//TODO: Get code and number of first transaction (to be used by read transaction)			
		}

		static BrowseQuery CreateBrowseQuery(BrowseDefinition browseDefinition)
		{
			var browseQuery = new BrowseQuery
			{
				Code = browseDefinition.Code,
				QueryColumns = browseDefinition.QueryColumns
			};

			ClearYearPeriodPrompt(browseQuery);
			SetTransactionDatePrompt(browseQuery);
			return browseQuery;
		}

		static void ClearYearPeriodPrompt(BrowseQuery browseQuery)
		{
			var column = browseQuery.QueryColumns.FindAskColumn("fin.trs.head.yearperiod");
			column.From = string.Empty;
			column.To = string.Empty;
		}

		static void SetTransactionDatePrompt(BrowseQuery browseQuery)
		{
			var column = browseQuery.QueryColumns.FindAskColumn("fin.trs.head.date");
			column.From = DateTime.Today.AddMonths(-1).ToString("yyyyMMdd");
			column.To = DateTime.Today.ToString("yyyyMMdd");
		}

		static void DisplayBrowseResult(BrowseResult result)
		{
			Console.WriteLine("Found {0} rows.", result.TotalNumberOfRows);
			Console.WriteLine(result.Columns.Select(c => c.Label).ToCommaSeparatedString());
			foreach (var row in result.Rows)
				Console.WriteLine(row.Cells.ToCommaSeparatedString());
			Console.WriteLine();
		}

		void ReadTransaction()
		{
			Console.WriteLine("Read transaction");

			const string daybook = "BNK";
			const int transactionNumber = 201300001;

			var transaction = bookkeepingService.ReadTransaction(daybook, transactionNumber);
			if (transaction == null)
			{
				Console.WriteLine("Transaction {0}, {1} not found.", daybook, transactionNumber);
				return;
			}

			DisplayTransaction(transaction);
			Console.WriteLine();
		}

		static void DisplayTransaction(Transaction transaction)
		{
			Console.WriteLine("Transaction:");
			Console.WriteLine("\tdaybook code = {0}", transaction.Daybook);
			Console.WriteLine("\tnumber = {0}", transaction.Number);
			Console.WriteLine("\tcurrency code = {0}", transaction.Currency);
			foreach (var line in transaction.Lines)
			{
				Console.WriteLine("\t\t amount = {0} {1}, dimensions = {2}",
						line.Amount, line.DebitCredit, line.Dimensions.ToCommaSeparatedString());
			}
		}
	}
}