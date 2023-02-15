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
		private static readonly Regex _hexColor = new Regex("#([0-9A-Fa-f]{8}|[0-9A-Fa-f]{6})");

		/// <summary>
		/// Instantiate an <see cref="Rgba32"/>.
		/// </summary>
		public Rgba32() { }

		/// <summary>
		/// Instantiate an <see cref="Rgba32"/> from individual channels.
		/// </summary>
		/// <param name="red">Red channel.</param>
		/// <param name="green">Green channel.</param>
		/// <param name="blue">Blue channel.</param>
		/// <param name="alpha">Alpha channel.</param>
		public Rgba32(byte red, byte green, byte blue, byte alpha)
		{
			R = red;
			G = green;
			B = blue;
			A = alpha;
		}

		/// <summary>
		/// Instantiate an <see cref="Rgba32"/> from a hex string.
		/// </summary>
		/// <param name="hex">Color hex string.</param>
		public Rgba32(string hex)
		{
			FromHex(hex);
		}

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
		/// Alpha channel.
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

		/// <summary>
		/// Get an interpolated color between the invoking one and another at a given position.
		/// </summary>
		/// <param name="color">Other color to interpolate between.</param>
		/// <param name="position">Position to interpolate in [0,1].</param>
		/// <returns></returns>
		public Rgba32 LinearInterpolate(Rgba32 color, double position)
		{
			if (color == null)
			{
				throw new ArgumentNullException(nameof(color));
			}
			else if (position < Constants.MinStopPosition || position > Constants.MaxStopPosition)
			{
				throw new ArgumentException(strings.InvalidPositionExceptionMessage);
			}

			var oneMinusPosition = 1.0 - position;
			return new Rgba32((byte)(oneMinusPosition * R + position * color.R),
					 (byte)(oneMinusPosition * G + position * color.G),
					 (byte)(oneMinusPosition * B + position * color.B),
					 (byte)(oneMinusPosition * A + position * color.A));
		}

		/// <summary>
		/// Returns a string that represents the <see cref="Rgba32"/>.
		/// </summary>
		/// <returns>A string that represents the <see cref="Rgba32"/></returns>
		public override string ToString()
		{
			return $"R: {R} G: {G} B: {B} A: {A}";
		}
	}
}
