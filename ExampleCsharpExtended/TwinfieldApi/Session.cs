namespace TwinfieldApi
{
	public class Session
	{
		public string SessionId { get; set; }
		public string ClusterUrl { get; set; }
		public string Office { get; set; }

		public bool LoggedOn => SessionId != null;

		public void Clear()
		{
			SessionId = null;
			ClusterUrl = null;
			Office = null;
		}
	}
}