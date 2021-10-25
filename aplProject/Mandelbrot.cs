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
        private Color[] palette;
        public Mandelbrot(int iterationLimit)
        {
            this.iterationLimit = iterationLimit;
            palette = new Color[16];
            generatePalette();
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
            palette[0] = Color.FromArgb(255, 66, 30, 15);
            palette[1] = Color.FromArgb(255, 25, 7, 26);
            palette[2] = Color.FromArgb(255, 9, 1, 47);
            palette[3] = Color.FromArgb(255, 4, 4, 73);
            palette[4] = Color.FromArgb(255, 0, 7, 100);
            palette[5] = Color.FromArgb(255, 12, 44, 138);
            palette[6] = Color.FromArgb(255, 24, 82, 177);
            palette[7] = Color.FromArgb(255, 57, 125, 209);
            palette[8] = Color.FromArgb(255, 134, 181, 229);
            palette[9] = Color.FromArgb(255, 211, 236, 248);
            palette[10] = Color.FromArgb(255, 241, 233, 191);
            palette[11] = Color.FromArgb(255, 248, 201, 95);
            palette[12] = Color.FromArgb(255, 255, 170, 0);
            palette[13] = Color.FromArgb(255, 204, 128, 0);
            palette[14] = Color.FromArgb(255, 153, 87, 0);
            palette[15] = Color.FromArgb(255, 106, 52, 3);
        }

        public Color getColor(int iteration)
        {
            if (iteration < iterationLimit)
            {
                return palette[iteration % 16];
            }
            else
            {
                return Color.Black;
            }

        }
    }
}
