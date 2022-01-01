using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplProject
{
    //Main interface of the project, It is responsible for calculations performed during fractal generation
    //Its implementations for C#, C++ and ASM are in Mandelbrot.cs file
    interface FractalGenerator
    {
        //Generate color palette which will will be used for coloring image
        void generatePalette();
        //Get color corresponding for given iteration
        System.Drawing.Color getColor(int interation);
        //For two complex numbers calculate iteration value
        //c stands for pixel which we check
        //x stands for start position (default 0,0)
        int countIterations(Complex c, Complex x);
    }
}
