using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Kinglet.Color.Tests
{
	[TestClass]
	public class Rgba32Tests
	{
		[TestMethod]
		public void FromHex_WithRRGGBB_SetsChannelsCorrectly()
		{
			var color = new Rgba32("#eb4334");

			Assert.AreEqual(235, color.R);
			Assert.AreEqual(67, color.G);
			Assert.AreEqual(52, color.B);
			Assert.AreEqual(255, color.A);
		}

		[TestMethod]
		public void FromHex_WithRRGGBBAA_SetsChannelsCorrectly()
		{
			var color = new Rgba32();

			color.FromHex("#80FCBA03");

			Assert.AreEqual(128, color.R);
			Assert.AreEqual(252, color.G);
			Assert.AreEqual(186, color.B);
			Assert.AreEqual(3, color.A);
		}

		[TestMethod]
		public void FromHex_WithShortHex_SetsChannelsCorrectly()
		{
			var color = new Rgba32();

			color.FromHex("#C38");

			Assert.AreEqual(204, color.R);
			Assert.AreEqual(51, color.G);
			Assert.AreEqual(136, color.B);
			Assert.AreEqual(255, color.A);
		}

		[TestMethod]
		public void FromHex_WithInvalidHex_ThrowsException()
		{
			var color = new Rgba32();

			var exception = Assert.ThrowsException<FormatException>(() => color.FromHex("#FFFFF"));

			Assert.AreEqual(strings.CouldNotParseHexExceptionMessage, exception.Message);
		}

		[TestMethod]
		public void ToHex_WithAlpha_ReturnsCorrectValue()
		{
			var color = new Rgba32(46, 38, 213, 56);

			var hex = color.ToHex(true);

			Assert.AreEqual("#2E26D538", hex);
		}

		[TestMethod]
		public void ToHex_WithoutAlpha_ReturnsCorrectValue()
		{
			var color = new Rgba32(143, 8, 111, 244);

			var hex = color.ToHex(false);

			Assert.AreEqual("#8F086F", hex);
		}

		[TestMethod]
		public void ToHex_WithDefaultAlpha_ReturnsCorrectValue()
		{
			var color = new Rgba32(229, 120, 4, 163);

			var hex = color.ToHex();

			Assert.AreEqual("#E57804A3", hex);
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
		public void LinearInterpolate_WithPositionTooSmall_ThrowsException()
		{
			var start = new Rgba32(255, 0, 128, 255);
			var end = new Rgba32(0, 255, 0, 128);

			var exception = Assert.ThrowsException<ArgumentException>(() => start.LinearInterpolate(end, -0.00001));

			Assert.AreEqual(strings.InvalidPositionExceptionMessage, exception.Message);
		}

		[TestMethod]
		public void LinearInterpolate_WithPositionTooLarge_ThrowsException()
		{
			var start = new Rgba32(255, 0, 128, 255);
			var end = new Rgba32(0, 255, 0, 128);

			var exception = Assert.ThrowsException<ArgumentException>(() => start.LinearInterpolate(end, 1.00001));

			Assert.AreEqual(strings.InvalidPositionExceptionMessage, exception.Message);
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