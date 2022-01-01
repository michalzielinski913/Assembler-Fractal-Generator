using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplProject
{
    //Complex number class for complex numbers calculations
    //However It is only used when c# is used for calculations
    class Complex
    {
        public double real;
        public double imaginary;

        public Complex(double a, double b)
        {
            real = a;
            imaginary = b;
        }

        public void Square()
        {
            double tmp = (real * real) - (imaginary * imaginary);
            imaginary = 2 * real * imaginary;
            real = tmp;
        }
        public double magnitude()
        {
            return ((real * real)+(imaginary*imaginary));
        }
        public void Add(Complex c)
        {
            real += c.real;
            imaginary += c.imaginary;
        }
    }
}
