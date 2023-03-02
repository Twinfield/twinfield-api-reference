using System;
using System.Linq;
using TwinfieldApi;
using TwinfieldApi.Organization;

namespace Demo
{
	class OrganizationDemo
	{
		readonly OrganizationService organizationService;

		public OrganizationDemo(Session session)
		{
			organizationService = new OrganizationService(session);
		}

		public void Run()
		{
			Console.WriteLine("Displaying office list");

			var offices = organizationService.GetOffices();
			Console.WriteLine("Found {0} offices:", offices.Count);

			Console.WriteLine("{0,-16} {1}", "Code", "Name");
			foreach (var office in offices.Take(10))
				Console.WriteLine("{0,-16} {1}", office.Code, office.Name);
			Console.WriteLine();
		}
	}
}