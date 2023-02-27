using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Services.Protocols;
using TwinfieldApi.Services;
using TwinfieldApi.Utilities;

namespace TwinfieldApi.Organization
{
	public class OrganizationService
	{
		readonly ProcessXmlService processXml;

		public OrganizationService(Session session)
		{
			processXml = new ProcessXmlService(session);
		}

		public List<OfficeSummary> GetOffices()
		{
			List<OfficeSummary> officeSummaries = null;
			try
			{
				var command = new ListOfficesCommand();
				var response = processXml.Process(command.ToXml());
				officeSummaries = OfficeSummaryList.FromXml(response);
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
			return officeSummaries;
		}
	}
}
