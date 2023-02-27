using System;
using System.Collections.Generic;
using System.Linq;
using TwinfieldApi;
using TwinfieldApi.Dimensions;

namespace Demo
{
	class DimensionDemo
	{
		const string CustomerDimensionType = "DEB";

		readonly DimensionService dimensionService;
		DimensionSummary customerSummary;
		Dimension customer;

		public DimensionDemo(Session session)
		{
			dimensionService = new DimensionService(session);
		}

		public void Run()
		{
			if (!FindCustomersWithNameThatStartsWithA())
				return;

			if (!ReadCustomerDetails())
				return;

			Console.WriteLine();
		}

		bool FindCustomersWithNameThatStartsWithA()
		{
			Console.WriteLine("Searching for customers with a name that starts with an A");

			const int searchField = 2; // Search in name field
			var customers = dimensionService.FindDimensions("a*", CustomerDimensionType, searchField);
			
			DisplayCustomerSummaries(customers);

			// Save first customer for later demo
			customerSummary = customers.First();
			return true;
		}

		bool ReadCustomerDetails()
		{
			Console.WriteLine("Read customer details");

			customer = dimensionService.ReadDimension(CustomerDimensionType, customerSummary.Code);

			if (customerSummary == null)
			{
				Console.WriteLine("Customer {0} not found.", customer.Code);
				return false;
			}
			
			DisplayCustomerDetails(customer);
			return true;
		}

		static void DisplayCustomerSummaries(IEnumerable<DimensionSummary> dimensions)
		{
			foreach (var dimension in dimensions)
				Console.WriteLine("{0,-16} {1}", dimension.Code, dimension.Name);
			Console.WriteLine();
		}

		static void DisplayCustomerDetails(Dimension dimension)
		{
			Console.WriteLine("Customer details:");
			Console.WriteLine("office = {0}", dimension.Office);
			Console.WriteLine("type = {0}", dimension.Type);
			Console.WriteLine("code = {0}", dimension.Code);
			Console.WriteLine("name = {0}", dimension.Name);
			Console.WriteLine();
		}

	}
}