using System;
using System.ComponentModel;

namespace Kinglet.Color
{
	/// <summary>
	/// A stop in a gradient.
	/// </summary>
	public class Stop
	{
		double _position;

		/// <summary>
		/// Instantiate a <see cref="Stop"/>.
		/// </summary>
		public Stop() { }

		/// <summary>
		/// Instantiate a <see cref="Stop"/>.
		/// </summary>
		/// <param name="position">The position in a gradient where the valid
		/// interval is [0, 1].</param>
		/// <param name="color">The color at the position.</param>
		public Stop(double position, Rgba32 color)
		{
			Position = position;
			Color = color;
		}


		/// <summary>
		/// The position in a gradient where the valid interval is [0, 1].
		/// </summary>
		public double Position
		{
			get
			{
				return _position;
			}
			set
			{
				if (value < Constants.MinStopPosition || value > Constants.MaxStopPosition)
				{
					throw new ArgumentException(strings.InvalidPositionExceptionMessage);
				}
				_position = value;
			}
		}

		/// <summary>
		/// The color at the position.
		/// </summary>
		public Rgba32 Color { get; set; }
	}
}
