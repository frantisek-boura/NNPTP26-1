using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    internal static class Renderer
    {
        private static Color[] Colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };

        private const int ColorMultiplier = 2;
        private const double ZeroReplacement = 0.0001;

        public static void RenderBitmap(Options options, Polynomial poly, Polynomial polyDerivative)
        {
            Bitmap bitmap = new Bitmap(options.BitmapWidth, options.BitmapHeight);

            for (int width = 0; width < options.BitmapWidth; width++)
            {
                for (int height = 0; height < options.BitmapHeight; height++)
                { 
                    double real = options.MinX + height * options.StepX;
                    double imaginary = options.MinY + width * options.StepY;
                    ComplexNumber pixel = new ComplexNumber()
                    {
                        Real = real == 0 ? ZeroReplacement : real,
                        Imaginary = imaginary == 0 ? ZeroReplacement : imaginary
                    };

                    IList<ComplexNumber> roots = new List<ComplexNumber>();
                    float iteration = NewtonSolver.Solve(poly, polyDerivative, pixel);
                    int colorId = NewtonSolver.FindRoot(pixel, roots);

                    Color color = GetColor(colorId, iteration);
                    bitmap.SetPixel(height, width, color);
                }
            }


            bitmap.Save(options.BitmapFilePath);
        }

        private static Color GetColor(int colorId, float iteration)
        {
            var color = Colors[colorId % Colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            color = Color.FromArgb(
                     Math.Min(Math.Max(color.R - (int)iteration * ColorMultiplier, 255), 0),
                     Math.Min(Math.Max(color.G - (int)iteration * ColorMultiplier, 255), 0),
                     Math.Min(Math.Max(color.B - (int)iteration * ColorMultiplier, 255), 0)
            );
            return color;
        }

    }
}
