using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Threading;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{

    class Program
    {
        static void Main(string[] args)
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

            var clrs = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            var maxid = 0;

            for (int width = 0; width < options.BitmapWidth; width++)
            {
                for (int height = 0; height < options.BitmapHeight; height++)
                { 
                    double real = options.MinX + height * options.StepX;
                    double imaginary = options.MinY + width * options.StepY;

                    ComplexNumber ox = new ComplexNumber()
                    {
                        Real = real == 0 ? 0.0001 : real,
                        Imaginary = imaginary == 0 ? 0.0001 : imaginary
                    };
                    IList<ComplexNumber> roots = new List<ComplexNumber>();


                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int q = 0; q< 30; q++)
                    {
                        var diff = p.Evaluate(ox).Divide(pd.Evaluate(ox));
                        ox = ox.Subtract(diff);

                        //Console.WriteLine($"{q} {ox} -({diff})");
                        if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                        {
                            q--;
                        }
                        it++;
                    }

                    //Console.ReadKey();

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w <koreny.Count;w++)
                    {
                        if (Math.Pow(ox.Real- koreny[w].Real, 2) + Math.Pow(ox.Imaginary - koreny[w].Imaginary, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }
                    if (!known)
                    {
                        koreny.Add(ox);
                        id = koreny.Count;
                        maxid = id + 1; 
                    }

                    // colorize pixel according to root number
                    //int vv = id;
                    //int vv = id * 50 + (int)it*5;
                    var vv = clrs[id % clrs.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R-(int)it*2), 255), Math.Min(Math.Max(0, vv.G - (int)it*2), 255), Math.Min(Math.Max(0, vv.B - (int)it*2), 255));
                    //vv = Math.Min(Math.Max(0, vv), 255);
                    bitmap.SetPixel(height, width, vv);
                    //bmp.SetPixel(j, i, Color.FromArgb(vv, vv, vv));
                }
            }


            bitmap.Save(options.BitmapFilePath);
        }
    }

}
