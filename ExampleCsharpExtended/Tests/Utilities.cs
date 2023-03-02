using TwinfieldApi;

namespace Tests
{
	static class Utilities
	{
		public static Session CreateValidSession()
		{
			return new Session
			{
				SessionId = "1234567890",
				Office = "001",
			};
		}
	}
}