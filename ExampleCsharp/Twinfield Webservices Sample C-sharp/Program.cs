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
			// Create the session.
			var session = new TwinfieldSession.Session();
			var clusterSession = new Session();

			// Request the user, password and organization.
			Console.WriteLine("Enter user name:");
			var user = Console.ReadLine().ToUpper();
			Console.WriteLine("Enter password:");
			var password = Console.ReadLine();
			Console.WriteLine("Enter organization:");
			var organization = Console.ReadLine().ToUpper();

			ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

			// Log on to the session.
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
					// Check the cluster URL.
					if (!string.IsNullOrEmpty(cluster))
						session.Url = cluster + "/webservices/session.asmx";

					var actionError = string.Empty;
					while (actionError == string.Empty && nextAction != LogonAction.None)
					{
						switch (nextAction)
						{
							case LogonAction.SMSLogon:
								// Send the code to the session user.
								var timeout = clusterSession.SmsSendCode();
								if (timeout > 0)
								{
									// Request the sms code.
									Console.WriteLine(
										"An SMS has been sent, please fill in the code within {0} seconds:",
										timeout);
									var code = Console.ReadLine();
									// Logon with the received sms code.
									var smsLogonResult =
										clusterSession.SmsLogon(code, out _);
									switch (smsLogonResult)
									{
										case SMSLogonResult.Invalid:
											actionError = "SMS log-on invalid.";
											break;
										case SMSLogonResult.TimeOut:
											actionError = "SMS code timed out.";
											break;
										case SMSLogonResult.Disabled:
											actionError = "Log-on is disabled.";
											break;
									}
								}
								else
									actionError = "SMS failed to send";

								break;
							case LogonAction.ChangePassword:
								// Request the passwords.
								Console.WriteLine("Current password:");
								var currentPassword = Console.ReadLine();
								Console.WriteLine("New password:");
								var newPassword = Console.ReadLine();
								// Change the password.

								var changePasswordResult =
									clusterSession.ChangePassword(currentPassword, newPassword, out _);
								switch (changePasswordResult)
								{
									case ChangePasswordResult.Invalid:
										actionError = "Current password is invalid.";
										break;
									case ChangePasswordResult.NotDifferent:
										actionError = "Passwords are not different.";
										break;
									case ChangePasswordResult.NotSecure:
										actionError = "New password is not secure.";
										break;
									case ChangePasswordResult.Disabled:
										actionError = "Log-on is disabled.";
										break;
								}

								break;
						}
					}

					if (actionError != string.Empty)
					{
						Console.WriteLine(actionError);
						Console.WriteLine("Press any key to exit...");
						Console.ReadKey();
						Environment.Exit(0);
					}
					else
						Console.WriteLine("Log-on successful.");

					break;
				case LogonResult.Blocked:
					Console.WriteLine("Log-on is blocked.");
					ExitProgram();
					break;
				case LogonResult.Untrusted:
					Console.WriteLine("Log-on is untrusted.");
					ExitProgram();
					break;
				case LogonResult.Invalid:
					Console.WriteLine("Log-on is invalid.");
					ExitProgram();
					break;
				case LogonResult.Deleted:
					Console.WriteLine("Log-on is deleted.");
					ExitProgram();
					break;
				case LogonResult.Disabled:
					Console.WriteLine("Log-on is disabled.");
					ExitProgram();
					break;
				case LogonResult.OrganisationInactive:
					Console.WriteLine("Organization is inactive.");
					ExitProgram();
					break;
				default:
					Console.WriteLine("Unknown log-on result.");
					ExitProgram();
					break;
			}
		}

		static void ExitProgram()
		{
			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
			Environment.Exit(0);
		}

		static void DisplayCompanies(string cluster, TwinfieldSession.Session session)
		{
			if (string.IsNullOrEmpty(cluster)) return;

			// Create instance of process xml web service.
			try
			{
				var processXml = new ProcessXml
				{
					// Create and assign the header.
					HeaderValue = new TwinfieldProcessXml.Header
					{
						SessionID = session.HeaderValue.SessionID
					}
				};

				// fill the request with xml as string
				const string xmlRequest = "<list><type>offices</type></list";

				// Set url
				processXml.Url = cluster + "/webservices/processxml.asmx";

				Console.WriteLine("ProcessXml: Displaying a list of offices... ");
				// ProcessXml as String
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
			if (string.IsNullOrEmpty(cluster)) return;

			try
			{
				clusterSession.Url = cluster + "/webservices/session.asmx";

				// Set sessionID header
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
			if (string.IsNullOrEmpty(cluster)) return;

			try
			{
				// Create instance of finder webservice
				var finder = new Finder();
				finder.Url = cluster + "/webservices/finder.asmx";

				// Set sessionID header
				finder.HeaderValue = new TwinfieldFinder.Header
				{
					SessionID = session.HeaderValue.SessionID
				};

				// Build options array
				var options = new string[2][];
				options[0] = new[] { "dimtype", "DEB" };
				options[1] = new[] { "section", "financials" };

				// Search dimensions of type "DEB" with bank account which starts with "1*", return the first 10 matches.
				Console.WriteLine("Displaying first 10 dimensions of type 'DEB' with bank account which starts with '1 * '");
				var messages = finder.Search("DIM", "1*", 3, 1, 10, options, out var results);
				if (messages.Any())
					foreach (var message in messages)
						Console.WriteLine(message.Text);
				else
				// Print results
				if (results.Items != null)
					for (var i = 0; i < results.Items.Length; ++i)
						Console.WriteLine("{0}: Customer {1} ({2})", i, results.Items[i][0],
							results.Items[i][1]);
				else
					Console.WriteLine("Nothing found.");

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
			if (string.IsNullOrEmpty(cluster)) return;

			try
			{
				// Abandon Twinfield session
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

			if (string.IsNullOrEmpty(statusDescription)) return;
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
