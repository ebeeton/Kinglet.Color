namespace Kinglet.Color.Tests
{
	[TestClass]
	public class GradientTests
	{
		[TestMethod]
		public void GetPalette_WithStops_ReturnsExpectedColors()
		{
			var black = new Rgba32(0, 0, 0, 255);
			var grey = new Rgba32(127, 127, 127, 255);
			var white = new Rgba32(255, 255, 255, 255);
			var paletteColors = 3;

			var gradient = new Gradient
			{
				Stops = new List<Stop>
				{
					// Black -> white, 100% alpha.
					new Stop(0, black),
					new Stop(1, white)
				}
			};

			var palette = gradient.GetPalette(paletteColors);

			Assert.AreEqual(paletteColors, palette.Length);
			Assert.IsTrue(black.Equals(palette[0]));
			Assert.IsTrue(grey.Equals(palette[1]));
			Assert.IsTrue(white.Equals(palette[2]));
		}
	}
}
