using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwinfieldApi.Dimensions;

namespace Tests
{
	[TestClass]
	public class DimensionServiceTests
	{
		DimensionService dimensionService;

		[TestInitialize]
		public void Initialize()
		{
			var factory = new TestClientFactory();
			var session = Utilities.CreateValidSession();
			dimensionService = new DimensionService(session, factory);
		}

		[TestMethod]
		public void Should_find_all_dimensions()
		{
			var dimensions = dimensionService.FindDimensions("1000", "DEB", 1);

			Assert.IsNotNull(dimensions);
		}

	}
}