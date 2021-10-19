using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplProject
{
    class Mandelbrot : FractalGenerator
    {
        private int iterationLimit;
        public Mandelbrot(int iterationLimit)
        {
            this.iterationLimit = iterationLimit;
        }
        public int countIterations(Complex c, Complex x)
        {
            int it = 0;
            do
            {
                it++;
                x.Square();
                x.Add(c);
                if (x.magnitude() > 2.0) break;
            } while (it < iterationLimit);
            return it;
        }

        public void generatePalette()
        {
            throw new NotImplementedException();
        }

        public Color getColor(int iteration)
        {
            return (iteration < iterationLimit ? System.Drawing.Color.Black : System.Drawing.Color.White);
        }
    }
}
