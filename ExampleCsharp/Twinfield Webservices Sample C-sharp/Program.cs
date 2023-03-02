using System;
using System.Linq;
using System.Net;
using System.Web.Services.Protocols;
using System.Xml;
using Twinfield_Webservices_Sample_C_sharp.TwinfieldClusterSession;
using Twinfield_Webservices_Sample_C_sharp.TwinfieldFinder;
using Twinfield_Webservices_Sample_C_sharp.TwinfieldProcessXml;
using Header = Twinfield_Webservices_Sample_C_sharp.TwinfieldClusterSession.Header;
using LogonAction = Twinfield_Webservices_Sample_C_sharp.TwinfieldSession.LogonAction;
using LogonResult = Twinfield_Webservices_Sample_C_sharp.TwinfieldSession.LogonResult;
using Session = Twinfield_Webservices_Sample_C_sharp.TwinfieldClusterSession.Session;

namespace Twinfield_Webservices_Sample_C_sharp
{
	class Program
	{
		static void Main(string[] args)
		{
			var session = new TwinfieldSession.Session();
			var clusterSession = new Session();

			Console.WriteLine("Enter user name:");
			var user = Console.ReadLine().ToUpper();
			Console.WriteLine("Enter password:");
			var password = Console.ReadLine();
			Console.WriteLine("Enter organization:");
			var organization = Console.ReadLine().ToUpper();

			ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

			var logonResult =
				session.Logon(user, password, organization, out var nextAction, out var cluster);

			ProcessLogonResult(logonResult, cluster, session, nextAction, clusterSession);

			DisplayCompanies(cluster, session);

			SelectCompany(cluster, clusterSession, session);

			DisplayCustomers(cluster, session);

			AbandonTwinfieldSession(cluster, clusterSession);

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		static void ProcessLogonResult(LogonResult logonResult, string cluster, TwinfieldSession.Session session,
			LogonAction nextAction, Session clusterSession)
		{
			switch (logonResult)
			{
				case LogonResult.Ok:
					if (!string.IsNullOrWhiteSpace(cluster))
						session.Url = cluster + "/webservices/session.asmx";
					ExecuteLogonAction(nextAction, clusterSession);
					break;
				case LogonResult.Blocked:
					ExitProgram("Log-on is blocked.");
					break;
				case LogonResult.Untrusted:
					ExitProgram("Log-on is untrusted.");
					break;
				case LogonResult.Invalid:
					ExitProgram("Log-on is invalid.");
					break;
				case LogonResult.Deleted:
					ExitProgram("Log-on is deleted.");
					break;
				case LogonResult.Disabled:
					ExitProgram("Log-on is disabled.");
					break;
				case LogonResult.OrganisationInactive:
					ExitProgram("Organization is inactive.");
					break;
				default:
					ExitProgram("Unknown log-on result.");
					break;
			}
		}

		static void ExecuteLogonAction(LogonAction nextAction, Session clusterSession)
		{
			var actionError = string.Empty;
			while (actionError == string.Empty && nextAction != LogonAction.None)
			{
				if (nextAction == LogonAction.SMSLogon)
				{
					var timeout = clusterSession.SmsSendCode();
					if (timeout > 0)
					{
						Console.WriteLine(
							"An SMS has been sent, please fill in the code within {0} seconds:",
							timeout);
						var code = Console.ReadLine();
						var smsLogonResult =
							clusterSession.SmsLogon(code, out _);
						if (smsLogonResult == SMSLogonResult.Invalid)
							actionError = "SMS log-on invalid.";
						else if (smsLogonResult == SMSLogonResult.TimeOut)
							actionError = "SMS code timed out.";
						else if (smsLogonResult == SMSLogonResult.Disabled)
							actionError = "Log-on is disabled.";
					}
					else
						actionError = "SMS failed to send";
				}
				else if (nextAction == LogonAction.ChangePassword)
				{
					Console.WriteLine("Current password:");
					var currentPassword = Console.ReadLine();
					Console.WriteLine("New password:");
					var newPassword = Console.ReadLine();

					var changePasswordResult =
						clusterSession.ChangePassword(currentPassword, newPassword, out _);
					if (changePasswordResult == ChangePasswordResult.Invalid)
						actionError = "Current password is invalid.";
					else if (changePasswordResult == ChangePasswordResult.NotDifferent)
						actionError = "Passwords are not different.";
					else if (changePasswordResult == ChangePasswordResult.NotSecure)
						actionError = "New password is not secure.";
					else if (changePasswordResult == ChangePasswordResult.Disabled)
						actionError = "Log-on is disabled.";
				}
			}

			if (actionError != string.Empty)
				ExitProgram(actionError);
			else
				Console.WriteLine("Log-on successful.");
		}

		static void ExitProgram(string message = null)
		{
			if (!string.IsNullOrWhiteSpace(message))
				Console.WriteLine(message);

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
			Environment.Exit(0);
		}

		static void DisplayCompanies(string cluster, TwinfieldSession.Session session)
		{
			if (string.IsNullOrWhiteSpace(cluster)) return;

			try
			{
				var processXml = new ProcessXml
				{
					HeaderValue = new TwinfieldProcessXml.Header
					{
						SessionID = session.HeaderValue.SessionID
					}
				};

				const string xmlRequest = "<list><type>offices</type></list>";

				processXml.Url = cluster + "/webservices/processxml.asmx";

				Console.WriteLine("ProcessXml: Displaying a list of offices... ");
				var xmlResult = processXml.ProcessXmlString(xmlRequest);
				Console.WriteLine(xmlResult);
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}
			catch (WebException ex)
			{
				HandleWebException(ex);
			}
			catch (SoapException ex)
			{
				HandleSoapException(ex);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error occurred while processing the xml request.");
				Console.WriteLine(ex.Message);
			}
		}

		static void SelectCompany(string cluster, Session clusterSession, TwinfieldSession.Session session)
		{
			if (string.IsNullOrWhiteSpace(cluster)) return;

			try
			{
				clusterSession.Url = cluster + "/webservices/session.asmx";

				clusterSession.HeaderValue = new Header
				{
					SessionID = session.HeaderValue.SessionID
				};

				Console.WriteLine("Enter company:");
				var company = Console.ReadLine().ToUpper();
				clusterSession.SelectCompany(company);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error occurred while processing the request.");
				Console.WriteLine(ex.Message);
			}
		}

		static void DisplayCustomers(string cluster, TwinfieldSession.Session session)
		{
			if (string.IsNullOrWhiteSpace(cluster)) return;

			try
			{
				var finder = new Finder();
				finder.Url = cluster + "/webservices/finder.asmx";

				finder.HeaderValue = new TwinfieldFinder.Header
				{
					SessionID = session.HeaderValue.SessionID
				};

				var options = new string[2][];
				options[0] = new[] { "dimtype", "DEB" };
				options[1] = new[] { "section", "financials" };

				Console.WriteLine("Displaying first 10 dimensions of type 'DEB' with bank account which starts with '1 * '");
				var messages = finder.Search("DIM", "1*", 3, 1, 10, options, out var results);
				if (messages.Any())
					foreach (var message in messages)
						Console.WriteLine(message.Text);
				else
				{
					if (results.Items != null)
						for (var i = 0; i < results.Items.Length; ++i)
							Console.WriteLine("{0}: Customer {1} ({2})", i, results.Items[i][0],
								results.Items[i][1]);
					else
						Console.WriteLine("Nothing found.");
				}

				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error occurred while processing the request.");
				Console.WriteLine(ex.Message);
			}
		}

		static void AbandonTwinfieldSession(string cluster, Session clusterSession)
		{
			if (string.IsNullOrWhiteSpace(cluster)) return;

			try
			{
				clusterSession.Abandon();
				Console.WriteLine("Twinfield session terminated.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error occurred while processing the request.");
				Console.WriteLine(ex.Message);
			}
		}

		static void HandleSoapException(SoapException soapException)
		{
			if (soapException == null) return;

			Console.WriteLine("Error occurred while processing the xml request.");
			if (soapException.Detail is XmlElement detail)
			{
				var el = (XmlElement)detail.SelectSingleNode("message");
				if (el != null) Console.WriteLine($"Message : {el.InnerText}");
				el = (XmlElement)detail.SelectSingleNode("code");
				if (el != null) Console.WriteLine($"Code : {el.InnerText}");
				el = (XmlElement)detail.SelectSingleNode("source");
				if (el != null) Console.WriteLine($"Source : {el.InnerText}");
				Console.WriteLine(detail.OuterXml);
				return;

			}

			Console.WriteLine($"Message : {soapException.Message}");
			Console.WriteLine($"Code : {soapException.Code}");
			Console.WriteLine($"SubCode : {(soapException.SubCode == null ? string.Empty : soapException.SubCode.ToString())}");
			Console.WriteLine($"Actor : {soapException.Actor}");
			Console.WriteLine($"Node : {soapException.Node}");
		}

		static void HandleWebException(WebException webException)
		{
			if (webException == null) return;

			Console.WriteLine("Error occurred while processing the xml request.");
			var statusCode = ((HttpWebResponse)webException.Response).StatusCode;
			Console.WriteLine($"Http status code : {statusCode}");

			if (statusCode != HttpStatusCode.Forbidden &&
				 statusCode != HttpStatusCode.Unauthorized) return;
			var statusDescription = ((HttpWebResponse)webException.Response).StatusDescription;

			if (string.IsNullOrWhiteSpace(statusDescription)) return;
			if (statusDescription.Contains(":"))
			{
				var descriptionDetails = statusDescription.Split(':');
				if (descriptionDetails.Length <= 1) return;
				Console.WriteLine($"Error code : {descriptionDetails[0].Trim()}");
				Console.WriteLine($"Error description : {descriptionDetails[1].Trim()}");
			}
			else
				Console.WriteLine($"Error description : {statusDescription}");
		}
	}
}
