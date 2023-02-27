using System;

namespace Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			var options = Options.Parse(args);

			if (options == null)
				ShowUsage();
			else
				RunDemo(options);

			WaitForUser();
		}

		static void ShowUsage()
		{
			Console.WriteLine("Demo.exe <user> <password> <organization> <office> [<url>]");
		}

		static void RunDemo(Options options)
		{
			try
			{
				var demo = new ApiDemo();
				demo.Run(options.Url, options.User, options.Password, options.Organization, options.Office);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		static void WaitForUser()
		{
			Console.WriteLine("Press any key to continue");
			Console.ReadKey();
		}
	}
}
