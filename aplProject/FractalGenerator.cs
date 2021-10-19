using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplProject
{
    interface FractalGenerator
    {
        void generatePalette();
        System.Drawing.Color getColor(int interation);

        int countIterations(Complex c, Complex x);

    }
}
