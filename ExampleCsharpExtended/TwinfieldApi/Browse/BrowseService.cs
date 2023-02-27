using System;
using System.Net;
using TwinfieldApi.Browse.Data;
using TwinfieldApi.Browse.Query;
using TwinfieldApi.Services;
using System.Web.Services.Protocols;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Browse
{
	public class BrowseService
	{
		readonly Session session;
		readonly ProcessXmlService processXml;

		public BrowseService(Session session)
		{
			this.session = session;
			processXml = new ProcessXmlService(session)
			{
				Compressed = true
			};
		}

		public BrowseDefinition ReadBrowseDefinition(string browseCode)
		{
			BrowseDefinition browseDefinition = null;
			var command = new ReadBrowseDefinitionCommand
			{
				Office = session.Office,
				Code = browseCode
			};
			try
			{
				var response = processXml.Process(command.ToXml());
				browseDefinition = BrowseDefinition.FromXml(response);
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
			return browseDefinition;
		}

		public BrowseResult Read(BrowseQuery query)
		{
			BrowseResult browseResult = null;
			try
			{
				var response = processXml.Process(query.ToXml());
				browseResult = BrowseResult.FromXml(response);
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

			return browseResult;
		}
	}
}
