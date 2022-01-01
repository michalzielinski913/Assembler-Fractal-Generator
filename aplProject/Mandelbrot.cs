using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace aplProject
{
    class MandelbrotCS : FractalGenerator
    {
        private int iterationLimit;
        private Color[] palette;
        public MandelbrotCS(int iterationLimit)
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
                if (x.magnitude() > 4.0) break;
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
    class MandelbrotCPP : FractalGenerator
    {
        private int iterationLimit;
        private Color[] palette;

        public int countIterationss(Complex c, Complex x)
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



        [DllImport("FractalGenerationCpp", CallingConvention = CallingConvention.Cdecl)]
        private static extern int countIterations(double real_one, double imaginary_one, double real_two, double imaginary_two, int iterationLimit);
        public int countIterations(Complex c, Complex x)
        {
            int tmp = countIterations(c.real, c.imaginary, x.real, x.imaginary, iterationLimit);

            return tmp;
        }

        public MandelbrotCPP(int iterationLimit)
        {
            this.iterationLimit = iterationLimit;
            palette = new Color[16];
            generatePalette();
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

    unsafe class MandelbrotASM : FractalGenerator
    {

        private int iterationLimit;
        private Color[] palette;

        [DllImport("Asm.dll")]
        private static extern int mandelbrot(double real_one, double imaginary_one, double real_two, double imaginary_two, int iterationLimit, double escape);
        public int countIterations(Complex c, Complex x)
        {
            int tmp = mandelbrot(c.real, c.imaginary, x.real, x.imaginary, iterationLimit, 4.0);
            return tmp;
        }
        public MandelbrotASM(int iterationLimit)
        {
            this.iterationLimit = iterationLimit;
            palette = new Color[16];
            generatePalette();
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
