using System.Collections.Generic;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynomial
        {
            public List<ComplexNumber> Coefficients { get; set; }

            public Polynomial() => Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber coe) => Coefficients.Add(coe);

            public Polynomial Derivative()
            {
                Polynomial p = new Polynomial();

                for (int i = 1; i < Coefficients.Count; i++)
                {
                    p.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
                }

                return p;
            }

            public ComplexNumber Evaluate(double x)
            {
                return Evaluate(new ComplexNumber() { Real = x, Imaginary = 0 });
            }

            public ComplexNumber Evaluate(ComplexNumber x)
            {
                ComplexNumber sum = ComplexNumber.Zero;

                for (int power = 0; power < Coefficients.Count; power++)
                {
                    ComplexNumber coefficient = Coefficients[power];
                    ComplexNumber currentX = x;

                    if (power > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            currentX = currentX.Multiply(x);

                        coefficient = coefficient.Multiply(currentX);
                    }

                    sum = sum.Add(coefficient);
                }

                return sum;
            }

            public override string ToString()
            {
                string output = "";
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    output += Coefficients[i];

                    if (i > 0)
                        for (int j = 0; j < i; j++)
                            output += "x";

                    if (i + 1 < Coefficients.Count) 
                        output += " + ";
                }

                return output;
            }
        }
    }
}
