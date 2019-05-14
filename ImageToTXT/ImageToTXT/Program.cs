using System;
using System.IO;
using System.Drawing;

namespace ImageToTXT
{
	class Program
	{
		public static void Main(string[] args)
		{
			string path = "";
			Bitmap img = null;
			if (args == null)
			{
				Console.WriteLine("Arguments were null!");
				return;
			}
			if (args.Length > 1 && args.Length != 0)
			{
				Console.WriteLine("CAN ONLY READ 1 FILE!");
				return;
			}
			if (args.Length == 1)
			{
				img = new Bitmap(args[0]);
				path = args[0];
			}
			if (args.Length == 0)
			{
				Console.WriteLine("Enter path:");
				path = Console.ReadLine();
				img = new Bitmap(path);
			}
			StreamWriter sw = new StreamWriter(Path.GetDirectoryName(path) + "\\output.txt");
			for (int y = 0; y < img.Height; y++)
			{
				for (int x = 0; x < img.Width; x++)
				{
					HSVColor p = HSVColor.GetHSV(img.GetPixel(x,y));
					double h = p.Hue;
					double s = p.Saturation;
					double b = p.Value;
					sw.WriteLine(Math.Floor(h));
					sw.WriteLine(Math.Floor(s));
					sw.WriteLine(Math.Floor(b));
				}
			}
		}
	}
	public struct HSVColor
	{
		public double Hue;
		public double Saturation;
		public double Value;
		public static HSVColor GetHSV (Color color)
		{
			HSVColor toReturn = new HSVColor();
		
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));
		
			toReturn.Hue = Math.Round(color.GetHue(), 2);
			toReturn.Saturation = ( ( max == 0 ) ? 0 : 1d - ( 1d * min / max ) ) * 100;
			toReturn.Saturation = Math.Round(toReturn.Saturation, 2);
			toReturn.Value = Math.Round(( ( max / 255d ) * 100 ), 2);
		
			return toReturn;
		}
	}
}