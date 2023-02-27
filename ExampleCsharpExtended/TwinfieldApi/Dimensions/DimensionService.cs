using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Services.Protocols;
using TwinfieldApi.Services;
using TwinfieldApi.TwinfieldFinderService;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Dimensions
{
	public class DimensionService
	{
		readonly Session session;
		readonly ProcessXmlService processXml;
		readonly FinderService finderService;

		public DimensionService(Session session)
			: this(session, new ClientFactory())
		{ }

		public DimensionService(Session session, IClientFactory clientFactory)
		{
			this.session = session;
			processXml = new ProcessXmlService(session, clientFactory);
			finderService = new FinderService(session, clientFactory);
		}

		public List<DimensionSummary> FindDimensions(string pattern, string dimensionType, int field)
		{
			var query = new FinderService.Query
			{
				Type = "DIM",
				Pattern = pattern,
				Field = field,
				MaxRows = 10,
				Options = new[] { new[] { "dimtype", dimensionType } }
			};
			var searchResult = finderService.Search(query);

			return SearchResultToDimensionSummaries(searchResult);
		}

		static List<DimensionSummary> SearchResultToDimensionSummaries(FinderData searchResult)
		{
			if (searchResult.Items == null)
				return new List<DimensionSummary>();

			return searchResult.Items.Select(item =>
					new DimensionSummary { Code = item[0], Name = item[1] }).ToList();
		}

		public Dimension ReadDimension(string dimensionType, string dimensionCode)
		{
			Dimension dimension = null;
			try
			{
				var command = new ReadDimensionCommand
				{
					Office = session.Office,
					DimensionType = dimensionType,
					DimensionCode = dimensionCode
				};
				var response = processXml.Process(command.ToXml());
				dimension =  Dimension.FromXml(response);
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
			return dimension;
		}

		public bool WriteDimension(Dimension dimension)
		{
			var result = false;
			try
			{
				var response = processXml.Process(dimension.ToXml());
				result = response.IsSuccess();
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
			return result;
		}
	}
}
