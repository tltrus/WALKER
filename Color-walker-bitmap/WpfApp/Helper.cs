using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawingVisualApp
{
    // Source is: https://github.com/LexaGV/WPFColorLib/tree/main
    internal static class Helper
    {
        public static (int, int, int) ColorToHSL(Color rgb)
        {
            var r = (rgb.R / 255.0);
            var g = (rgb.G / 255.0);
            var b = (rgb.B / 255.0);

            var min = Math.Min(Math.Min(r, g), b);
            var max = Math.Max(Math.Max(r, g), b);
            var delta = max - min;

            var lum = (max + min) / 2;
            double hue, sat;

            if (delta == 0)
            {
                hue = 0.0;
                sat = 0.0;
            }
            else
            {
                sat = (lum <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));

                if (r == max)
                {
                    hue = ((g - b) / 6.0) / delta;
                }
                else if (g == max)
                {
                    hue = (1.0 / 3.0) + ((b - r) / 6.0) / delta;
                }
                else
                {
                    hue = (2.0 / 3.0) + ((r - g) / 6.0) / delta;
                }

                if (hue < 0)
                    hue += 1;
                if (hue > 1)
                    hue -= 1;
            }

            return ((int)(hue * 360), (int)(sat * 100), (int)(lum * 100));
        }

        public static void HsvToRgb(double h, double S, double V, out byte r, out byte g, out byte b)
        {


            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {



                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;



                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;



                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;



                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;



                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;



                    default:

                        R = G = B = V;
                        break;
                }
            }
            r = Clamp((byte)(R * 255.0));
            g = Clamp((byte)(G * 255.0));
            b = Clamp((byte)(B * 255.0));
        }


        private static byte Clamp(byte i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }
}
