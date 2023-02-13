namespace Kinglet.Color.Tests
{
	[TestClass]
	public class GradientTests
	{
		private readonly Rgba32 black = new(0, 0, 0, 255);
		private readonly Rgba32 white = new(255, 255, 255, 255);

		[TestMethod]
		public void GetPalette_WithValidStops_ReturnsExpectedColors()
		{
			var grey = new Rgba32(127, 127, 127, 255);

			var gradient = new Gradient
			{
				Stops = new List<GradientStop>
				{
					// Black -> white, 100% alpha.
					new GradientStop(0, black),
					new GradientStop(1, white)
				}
			};

			var palette = gradient.GetPalette(3);

			Assert.AreEqual(3, palette.Length);
			Assert.AreEqual(black.R, palette[0].R);
			Assert.AreEqual(black.G, palette[0].G);
			Assert.AreEqual(black.B, palette[0].B);
			Assert.AreEqual(black.A, palette[0].A);
			Assert.AreEqual(grey.R, palette[1].R);
			Assert.AreEqual(grey.G, palette[1].G);
			Assert.AreEqual(grey.B, palette[1].B);
			Assert.AreEqual(grey.A, palette[1].A);
			Assert.AreEqual(white.R, palette[2].R);
			Assert.AreEqual(white.G, palette[2].G);
			Assert.AreEqual(white.B, palette[2].B);
			Assert.AreEqual(white.A, palette[2].A);
		}

		[TestMethod]
		public void GetPalette_WithLessThanTwoStops_ThrowsException()
		{
			var gradient = new Gradient();

			var exception = Assert.ThrowsException<InvalidOperationException>(() => gradient.GetPalette(10));

			Assert.AreEqual(strings.InvalidGradientStopCountExceptionMessage, exception.Message);
		}

		[TestMethod]
		public void GetPalette_WithFirstStopPositionNotZero_ThrowsException()
		{
			var gradient = new Gradient
			{
				Stops = new List<GradientStop>
				{
					new GradientStop(0.1, black),
					new GradientStop(1, white)
				}
			};

			var exception = Assert.ThrowsException<InvalidOperationException>(() => gradient.GetPalette(10));

			Assert.AreEqual(strings.InvalidGradientStopFirstPositionExceptionMessage, exception.Message);
		}

		[TestMethod]
		public void GetPalette_WithLastStopPositionNotOne_ThrowsException()
		{
			var gradient = new Gradient
			{
				Stops = new List<GradientStop>
				{
					new GradientStop(0, black),
					new GradientStop(0.9, white)
				}
			};

			var exception = Assert.ThrowsException<InvalidOperationException>(() => gradient.GetPalette(10));

			Assert.AreEqual(strings.InvalidGradientStopLastPositionExceptionMessage, exception.Message);
		}
	}
}
