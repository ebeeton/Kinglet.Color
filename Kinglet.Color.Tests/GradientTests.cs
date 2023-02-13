namespace Kinglet.Color.Tests
{
	[TestClass]
	public class GradientTests
	{
		private readonly Rgba32 _black = new(0, 0, 0, 255);
		private readonly Rgba32 _white = new(255, 255, 255, 255);
		private readonly Rgba32 _grey = new(127, 127, 127, 255);
		private readonly Rgba32 _green = new(0, 0, 255, 255);

		[TestMethod]
		public void GetPalette_WithValidStops_ReturnsExpectedColors()
		{
			var gradient = new Gradient
			{
				Stops = new List<GradientStop>
				{
					// Black -> white, 100% alpha.
					new GradientStop(0, _black),
					new GradientStop(1, _white)
				}
			};

			var palette = gradient.GetPalette(3);

			Assert.AreEqual(3, palette.Length);
			Assert.AreEqual(_black.R, palette[0].R);
			Assert.AreEqual(_black.G, palette[0].G);
			Assert.AreEqual(_black.B, palette[0].B);
			Assert.AreEqual(_black.A, palette[0].A);
			Assert.AreEqual(_grey.R, palette[1].R);
			Assert.AreEqual(_grey.G, palette[1].G);
			Assert.AreEqual(_grey.B, palette[1].B);
			Assert.AreEqual(_grey.A, palette[1].A);
			Assert.AreEqual(_white.R, palette[2].R);
			Assert.AreEqual(_white.G, palette[2].G);
			Assert.AreEqual(_white.B, palette[2].B);
			Assert.AreEqual(_white.A, palette[2].A);
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
