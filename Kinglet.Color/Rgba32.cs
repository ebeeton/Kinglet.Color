using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Kinglet.Color
{
	/// <summary>
	/// An RGBA color with 8 bits per channel.
	/// </summary>
	public class Rgba32
	{
		private readonly Regex _hexColor = new Regex("#([0-9A-Fa-f]{8}|[0-9A-Fa-f]{6})");

		/// <summary>
		/// Red channel.
		/// </summary>
		public byte R { get; set; }

		/// <summary>
		/// Green channel.
		/// </summary>
		public byte G { get; set; }

		/// <summary>
		/// Blue channel.
		/// </summary>
		public byte B { get; set; }

		/// <summary>
		/// Alpha channel
		/// </summary>
		public byte A { get; set; } = byte.MaxValue;

		/// <summary>
		/// Converts a color hex string to an <see cref="RGBA32"/>.
		/// </summary>
		/// <param name="hex">Color hex string.</param>
		/// <remarks>
		/// If the hex string is of the form #FFFFFF, the alpha channel will
		/// default to opaque (255). If is of the form #FFFFFFFF, the alpha
		/// channel will be set to the first byte.
		/// </remarks>
		public void FromHex(string hex)
		{
			if (string.IsNullOrWhiteSpace(hex))
			{
				throw new ArgumentNullException(nameof(hex));
			}

			var match = _hexColor.Match(hex);
			if (!match.Success || match.Groups.Count != 2)
			{
				Trace.WriteLine($"{nameof(FromHex)} failed to to parse \"{hex}\".");
				throw new FormatException(strings.CouldNotParseHexExceptionMessage);
			}

			var hexOnly = match.Groups[1].Value;
			var bytes = new List<byte>();
			for (int i = 0; i < hexOnly.Length; i += 2)
			{
				var eachByte = Convert.ToByte(hexOnly.Substring(i, 2), 16);
				bytes.Add(eachByte);
			}

			if (bytes.Count == 4)
			{
				A = bytes[0];
				R = bytes[1];
				G = bytes[2];
				B = bytes[3];
			}
			else
			{
				A = byte.MaxValue;
				R = bytes[0];
				G = bytes[1];
				B = bytes[2];
			}
			Trace.WriteLine($"{nameof(FromHex)} parsed {hex} to A:{A} R:{R} G:{G} B:{B}.");
		}
	}
}
