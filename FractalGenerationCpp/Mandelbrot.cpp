#define MandelBrot _declspec(dllexport)
#include <math.h>
extern "C" {

	MandelBrot int countIterations(double real_one, double imaginary_one, double real_two, double imaginary_two, int iterationLimit) {
		int it = 0;
		do {
			it++;
            double tmp = (real_two * real_two) - (imaginary_two * imaginary_two);
            imaginary_two = 2 * real_two * imaginary_two;
            real_two = tmp;
            real_two += real_one;
            imaginary_two += imaginary_one;
            if ((sqrt(real_two * real_two) + (imaginary_two * imaginary_two)) > 2.0) break;
		} while (it < iterationLimit);
		return it;
	}

}