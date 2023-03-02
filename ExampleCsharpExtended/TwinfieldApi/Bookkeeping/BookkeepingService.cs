using System;
using System.Net;
using System.Web.Services.Protocols;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Bookkeeping
{
	public class BookkeepingService
	{
		readonly ProcessXmlService processXml;
		readonly Session session;

		public BookkeepingService(Session session)
		{
			this.session = session;
			processXml = new ProcessXmlService(session);
		}

		public Transaction ReadTransaction(string daybook, decimal transactionNumber)
		{
			Transaction transaction = null;
			var command = new ReadTransactionCommand
			{
				Office = session.Office,
				Daybook = daybook,
				TransactionNumber = transactionNumber
			};
			try
			{
				var response = processXml.Process(command.ToXml());
				transaction = Transaction.FromXml(response);
			}
			catch (WebException ex)
			{
				ex.HandleWebException();
			}
			catch (SoapException ex)
			{
				ex.HandleSoapException();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error occurred while processing the xml request.");
				Console.WriteLine(ex.Message);
			}

			return transaction;
		}

		public void BrowseTransactions()
		{
			//TODO: Implement browse transactions
		}

	}
}
