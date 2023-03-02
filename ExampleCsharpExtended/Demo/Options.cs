namespace Demo
{
	class Options
	{
		const string DefaultLoginServerUrl = "https://login.twinfield.com";

		public string User { get; set; }
		public string Password { get; set; }
		public string Organization { get; set; }
		public string Office { get; set; }
		public string Url { get; set; }

		public static Options Parse(string[] args)
		{
			if (args.Length < 4)
				return null;

			return new Options
			{
				User = args[0].ToUpper(),
				Password = args[1],
				Organization = args[2].ToUpper(),
				Office = args[3].ToUpper(),
				Url = args.Length > 4 ? args[4] : DefaultLoginServerUrl
			};
		}
	}
}