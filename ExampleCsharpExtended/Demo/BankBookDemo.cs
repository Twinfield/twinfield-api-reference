using System;
using TwinfieldApi;
using TwinfieldApi.Bankbooks;

namespace Demo
{
	class BankBookDemo
	{
		readonly BankbookService bankBookService;

		public BankBookDemo(Session session)
		{
			bankBookService = new BankbookService(session);
		}

		public void Run()
		{
			Console.WriteLine("Read bank book");

			var bankBook = bankBookService.FindBankBook("BNK");
			if (bankBook == null)
			{
				Console.WriteLine("Bank book not found.");
			}
			else
			{
				Console.WriteLine("Bank book found");
				Console.WriteLine("\tcode = {0}", bankBook.Code);
				Console.WriteLine("\tname = {0}", bankBook.Name);
				Console.WriteLine("\taccount number = {0}", bankBook.AccountNumber);
				Console.WriteLine("\tIBAN = {0}", bankBook.Iban);
			}

			Console.WriteLine();
		}
	}
}