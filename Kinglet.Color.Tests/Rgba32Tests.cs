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
		public void FromHexColor_WithInvalidHex_ThrowsException()
		{
			var color = new Rgba32();

			var exception = Assert.ThrowsException<FormatException>(() => color.FromHex("#FFFFF"));

			Assert.AreEqual(strings.CouldNotParseHexExceptionMessage, exception.Message);
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

		[TestMethod]
		public void LinearInterpolate_WithColorAndPosition_ReturnsInterpolatedColor()
		{
			var start = new Rgba32(255, 0, 128, 255);
			var end = new Rgba32(0, 255, 0, 128);

			var interpolated = start.LinearInterpolate(end, 0.5);

			Assert.AreEqual(127, interpolated.R);
			Assert.AreEqual(127, interpolated.G);
			Assert.AreEqual(64, interpolated.B);
			Assert.AreEqual(191, interpolated.A);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void LinearInterpolate_WithPositionTooSmall_ThrowsException()
		{
			var start = new Rgba32(255, 0, 128, 255);
			var end = new Rgba32(0, 255, 0, 128);

			var interpolated = start.LinearInterpolate(end, -0.00001);

			Assert.Fail("An exception was not thrown.", interpolated);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void LinearInterpolate_WithPositionTooLarge_ThrowsException()
		{
			var start = new Rgba32(255, 0, 128, 255);
			var end = new Rgba32(0, 255, 0, 128);

			var interpolated = start.LinearInterpolate(end, 1.00001);

			Assert.Fail("An exception was not thrown.", interpolated);
		}

		[TestMethod]
		public void LinearInterpolate_WithPosition0_ReturnsFirstColor()
		{
			var start = new Rgba32(255, 0, 128, 255);
			var end = new Rgba32(0, 255, 0, 128);

			var interpolated = start.LinearInterpolate(end, 0);

			Assert.AreEqual(255, interpolated.R);
			Assert.AreEqual(0, interpolated.G);
			Assert.AreEqual(128, interpolated.B);
			Assert.AreEqual(255, interpolated.A);
		}

		[TestMethod]
		public void LinearInterpolate_WithPosition1_ReturnsSecondColor()
		{
			var start = new Rgba32(255, 0, 128, 255);
			var end = new Rgba32(0, 255, 0, 128);

			var interpolated = start.LinearInterpolate(end, 1.0);

			Assert.AreEqual(0, interpolated.R);
			Assert.AreEqual(255, interpolated.G);
			Assert.AreEqual(0, interpolated.B);
			Assert.AreEqual(128, interpolated.A);
		}
	}
}