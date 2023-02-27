using TwinfieldApi.TwinfieldBankBookService;

namespace TwinfieldApi.BankBooks
{
	public class BankBook
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string AccountNumber { get; set; }
		public string Iban { get; set; }

		public static BankBook FromQueryResult(string bankCode, QueryResult queryResult)
		{
			var result = queryResult as GetBankBookResult;
			if (result == null)
				return null;

			return new BankBook
			{
				Code = bankCode.ToUpper(),
				Name = result.Name,
				AccountNumber = result.BankAccount.Number,
				Iban = result.BankAccount.Iban
			};
		}
	}
}