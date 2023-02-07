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

			var gradient = new Gradient
			{
				Stops = new List<Stop>
				{
					// Black -> white, 100% alpha.
					new Stop(0, black),
					new Stop(1, white)
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
	}
}
