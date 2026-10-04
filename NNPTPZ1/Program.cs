using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Options options = UserInputParser.Parse(args);
                Polynomial poly = new Polynomial
                (
                        new ComplexNumber() { Real = 1 },
                        ComplexNumber.Zero,
                        ComplexNumber.Zero,
                        new ComplexNumber() { Real = 1 }
                );
                Polynomial polyDerivative = poly.Derivative();
                Bitmap bitmap = new Bitmap(options.BitmapWidth, options.BitmapHeight);

                Console.WriteLine(poly);
                Console.WriteLine(polyDerivative);

                Renderer.RenderBitmap(options, poly, polyDerivative);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
