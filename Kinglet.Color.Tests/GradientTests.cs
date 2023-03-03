namespace Kinglet.Color.Tests
{
	[TestClass]
	public class GradientTests
	{
		private readonly Rgba32 _black = new(0, 0, 0, 255);
		private readonly Rgba32 _white = new(255, 255, 255, 255);
		private readonly Rgba32 _grey = new(127, 127, 127, 255);
		private readonly Rgba32 _green = new(0, 255, 0, 255);
		private readonly Rgba32 _translucentBlue = new(0, 0, 255, 128);

		[TestMethod]
		public void GetPalette_WithValidStops_ReturnsExpectedColors()
		{
			var gradient = new Gradient
			{
				Stops = new List<GradientStop>
				{
					new GradientStop(0, _green),
					new GradientStop(0.5, _translucentBlue),
					new GradientStop(1, _grey)
				}
			};
			const int PaletteColors = 9;
			var expectedColors = new string[PaletteColors]
			{
				"#00FF00FF",
				"#00BF3FDF",
				"#007F7FBF",
				"#003FBF9F",
				"#0000FF80",
				"#1F1FDF9F",
				"#3F3FBFBF",
				"#5F5F9FDF",
				"#7F7F7FFF",
			};

			var palette = gradient.GetPalette(PaletteColors);

			Assert.AreEqual(PaletteColors, palette.Length);
			for (int i = 0; i < PaletteColors; ++i)
			{
				Assert.AreEqual(expectedColors[i], palette[i].ToHex());
			}
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
					new GradientStop(0.1, _black),
					new GradientStop(1, _white)
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
					new GradientStop(0, _black),
					new GradientStop(0.9, _white)
				}
			};

			var exception = Assert.ThrowsException<InvalidOperationException>(() => gradient.GetPalette(10));

			Assert.AreEqual(strings.InvalidGradientStopLastPositionExceptionMessage, exception.Message);
		}

		[TestMethod]
		public void GetPalette_WithNonAscendingStopPosition_ThrowsException()
		{
			var gradient = new Gradient
			{
				Stops = new List<GradientStop>
				{
					new GradientStop(0, _black),
					new GradientStop(0.5, _grey),
					// Out of order.
					new GradientStop(0.25, _green),
					new GradientStop(1, _white)
				}
			};

			var exception = Assert.ThrowsException<InvalidOperationException>(() => gradient.GetPalette(10));

			Assert.AreEqual(strings.DuplicateGradientStopPositionExceptionMessage, exception.Message);
		}

		[TestMethod]
		public void GetPalette_WithDuplicateStopPosition_ThrowsException()
		{
			var gradient = new Gradient
			{
				Stops = new List<GradientStop>
				{
					new GradientStop(0, _black),
					new GradientStop(0.5, _grey),
					// Duplicate position.
					new GradientStop(1, _green),
					new GradientStop(1, _white)
				}
			};

			var exception = Assert.ThrowsException<InvalidOperationException>(() => gradient.GetPalette(10));

			Assert.AreEqual(strings.DuplicateGradientStopPositionExceptionMessage, exception.Message);
		}
	}
}
