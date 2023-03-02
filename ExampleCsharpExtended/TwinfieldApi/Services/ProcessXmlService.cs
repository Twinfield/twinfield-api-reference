using System;
using System.Linq;
using System.Xml;
using TwinfieldApi.Utilities;
using Header = TwinfieldApi.TwinfieldProcessXmlService.Header;

namespace TwinfieldApi.Services
{
	public class ProcessXmlService
	{
		readonly Session session;
		readonly IClientFactory clientFactory;

		public ProcessXmlService(Session session)
			: this(session, new ClientFactory())
		{ }

		public ProcessXmlService(Session session, IClientFactory clientFactory)
		{
			this.session = session;
			this.clientFactory = clientFactory;
		}

		public bool Compressed { get; set; }

		public XmlElement Process(XmlDocument input)
		{
			var client = clientFactory.CreateProcessXmlClient(session.ClusterUrl);
			var header = new Header { SessionID = session.SessionId };
			var result = Process(client, header, input);
			if (!result.IsSuccess())
				throw new ProcessXmlException(result);
			return result;
		}

		XmlElement Process(IProcessXmlSoapClient client, Header header, XmlDocument input)
		{
			return Compressed
				? ProcessCompressed(client, header, input)
				: ProcessUncompressed(client, header, input);
		}

		static XmlElement ProcessUncompressed(IProcessXmlSoapClient client, Header header, XmlDocument input)
		{
			return client.ProcessXmlDocument(header, input) as XmlElement;
		}

		static XmlElement ProcessCompressed(IProcessXmlSoapClient client, Header header, XmlDocument input)
		{
			var compressedInput = Zlib.CompressXml(input);
			var compressedOutput = client.ProcessXmlCompressed(header, compressedInput);
			var document = Zlib.DecompressXml(compressedOutput);
			return document?.DocumentElement;
		}
	}

	internal class ProcessXmlException : Exception
	{
		readonly XmlElement element;

		public ProcessXmlException(XmlElement element)
		{
			this.element = element;
		}

		public ProcessXmlException(XmlDocument document)
		{
			if (document != null)
				element = document.DocumentElement;
		}

		public override string Message
		{
			get
			{
				if (element == null)
					return "Process XML didn't return any XML.";
				var messages = element.GetMessages();
				if (!messages.Any())
					return "Process XML returned XML that contains an unknown error.";
				return string.Join(" ", messages.ToArray());
			}
		}
	}
}
