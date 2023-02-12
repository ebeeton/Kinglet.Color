namespace Kinglet.Color.Tests
{
	[TestClass]
	public class GradientStopTests
	{
		[TestMethod]
		public void PositionSet_WithValidInterval_GetsCorrectValue()
		{
			var stop = new GradientStop(0.333, new Rgba32());

			var position = stop.Position;

			Assert.AreEqual(0.333, position);
		}

		[TestMethod]
		public void PositionSet_TooSmall_ThrowsException()
		{
			var exception = Assert.ThrowsException<ArgumentException>(() => new GradientStop(-0.00001, new Rgba32()));

			Assert.AreEqual(strings.InvalidPositionExceptionMessage, exception.Message);
		}

		[TestMethod]
		public void PositionSet_TooLarge_ThrowsException()
		{
			var exception = Assert.ThrowsException<ArgumentException>(() => new GradientStop
			{
				Position = 1.00001
			});

			Assert.AreEqual(strings.InvalidPositionExceptionMessage, exception.Message);
		}
	}
}
