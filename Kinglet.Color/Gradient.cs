﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kinglet.Color
{
	/// <summary>
	/// A color gradient defined as collection of stops.
	/// </summary>
	public class Gradient
	{
		/// <summary>
		/// The stops that make up the <see cref="Gradient"/>.
		/// </summary>
		public List<GradientStop> Stops { get; set; } = new List<GradientStop>();

		/// <summary>
		/// Gets a palette with a given number of colors in the same order as the gradient stops.
		/// </summary>
		/// <param name="count">Number of colors in the resulting palette.</param>
		/// <returns>A palette of colors representi</returns>
		public Rgba32[] GetPalette(UInt32 count)
		{
			Validate();
			var palette = new Rgba32[count];

			// Ensure that the last color is the last stop's color.
			var step = 1.0 / ((double)count - 1);
			Parallel.For(0, count, i => palette[i] = GetInterpolatedColor(i * step));
			return palette;
		}

		private Rgba32 GetInterpolatedColor(double position)
		{
			GradientStop first, second;
			for (int i = 0; i < Stops.Count - 1; i++)
			{
				// Which stops is position between?
				first = Stops[i];
				second = Stops[i + 1];
				if (first.Position <= position && position <= second.Position)
				{
					// Position is in [0,1] over all stops, scale it to the range between the two colors.
					var localPosition = (position - first.Position) / (second.Position - first.Position);
					return first.Color.LinearInterpolate(second.Color, localPosition);
				}
			}

			return Stops[Stops.Count - 1].Color;
		}

		private void Validate()
		{
			if (Stops.Count < 2)
			{
				throw new InvalidOperationException(strings.InvalidGradientStopCountExceptionMessage);
			}
			else if (Stops.First().Position != Constants.MinStopPosition)
			{
				throw new InvalidOperationException(strings.InvalidGradientStopFirstPositionExceptionMessage);
			}
			else if (Stops.Last().Position != Constants.MaxStopPosition)
			{
				throw new InvalidOperationException(strings.InvalidGradientStopLastPositionExceptionMessage);
			}

			// Ensure each stop position is greater than the last.
			for (int i = 0; i < Stops.Count - 1; i++)
			{
				if (Stops[i].Position >= Stops[i + 1].Position)
				{
					throw new InvalidOperationException(strings.DuplicateGradientStopPositionExceptionMessage);
				}
			}
		}
	}
}
