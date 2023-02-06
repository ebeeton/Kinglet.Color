using System.Collections.Generic;

namespace Kinglet.Color
{
	/// <summary>
	/// A color gradient defined as collection of stops.
	/// </summary>
	public class Gradient
	{
		/// <summary>
		/// The stops that make up the gradient.
		/// </summary>
		public List<Stop> Stops { get; set; }
	}
}
