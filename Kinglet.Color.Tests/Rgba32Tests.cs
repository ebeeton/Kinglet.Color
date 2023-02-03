namespace Kinglet.Color.Tests
{
	[TestClass]
	public class Rgba32Tests
	{
		[TestMethod]
		public void FromHexColor_WithWhite_SetsChannelsCorrectly()
		{
			var color = new Rgba32();

			color.FromHex("#FFFFFF");

			Assert.AreEqual(byte.MaxValue, color.R);
			Assert.AreEqual(byte.MaxValue, color.G);
			Assert.AreEqual(byte.MaxValue, color.B);
			Assert.AreEqual(byte.MaxValue, color.A);
		}

		[TestMethod]
		public void FromHexColor_WithColor_SetsChannelsCorrectly()
		{
			var color = new Rgba32();

			color.FromHex("#80FCBA03");

			Assert.AreEqual(252, color.R);
			Assert.AreEqual(186, color.G);
			Assert.AreEqual(3, color.B);
			Assert.AreEqual(128, color.A);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void FromHexColor_WithInvalidHex_ThrowsException()
		{
			var color = new Rgba32();

			color.FromHex("#FFFFF");

			Assert.Fail();
		}

		[TestMethod]
		public void Rgba32_WithNoArguments_DefaultsToOpaqueBlack()
		{
			var color = new Rgba32();

			Assert.AreEqual(0, color.R);
			Assert.AreEqual(0, color.G);
			Assert.AreEqual(0, color.B);
			Assert.AreEqual(byte.MaxValue, color.A);
		}
	}
}