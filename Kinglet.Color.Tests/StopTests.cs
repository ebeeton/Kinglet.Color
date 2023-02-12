namespace Kinglet.Color.Tests
{
	[TestClass]
	public class StopTests
	{
		[TestMethod]
		public void PositionSet_WithValidInterval_GetsCorrectValue()
		{
			var stop = new GradientStop(0.333, new Rgba32());

			var position = stop.Position;

			Assert.AreEqual(0.333, position);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PositionSet_TooSmall_ThrowsException()
		{
			var stop = new GradientStop(-0.00001, new Rgba32());

			Assert.Fail("An exception was not thrown.", stop);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PositionSet_TooLarge_ThrowsException()
		{
			var stop = new GradientStop
			{
				Position = 1.00001
			};

			Assert.Fail("An exception was not thrown.", stop);
		}
	}
}
