using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    internal static class NewtonSolver
    {

        private const int NewtonIterations = 30;
        private const double SolutionThreshold = 0.5;
        private const double RootThreshold = 0.01;

        public static double Solve(Polynomial poly, Polynomial polyDerivative, ComplexNumber pixel)
        {
            double iteration = 0;
            for (int i = 0; i < NewtonIterations; i++)
            {
                var difference = poly.Evaluate(pixel).Divide(polyDerivative.Evaluate(pixel));
                pixel = pixel.Subtract(difference);

                if (Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= SolutionThreshold)
                    i--;
                iteration++;
            }

            return iteration;
        }

        public static int FindRoot(ComplexNumber pixel, IList<ComplexNumber> roots)
        {
            int solution = 0;
            var known = false;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(pixel.Real - roots[i].Real, 2) + Math.Pow(pixel.Imaginary - roots[i].Imaginary, 2) <= RootThreshold)
                {
                    known = true;
                    solution = i;
                }
            }

            if (!known)
            {
                roots.Add(pixel);
                solution = roots.Count;
            }

            return solution;
        }
    }
}
