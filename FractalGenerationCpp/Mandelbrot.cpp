#define MandelBrot _declspec(dllexport)
#include <math.h>
extern "C" {

	//Count iterations for given complex numbers
	//It looks simillar to c# solution however we dont have access to Complex class so we split them to real and imaginary parts
	MandelBrot int countIterations(double real_one, double imaginary_one, double real_two, double imaginary_two, int iterationLimit) {
		int it = 0;
		do {
			it++;
            double tmp = (real_two * real_two) - (imaginary_two * imaginary_two);
            imaginary_two = 2 * real_two * imaginary_two;
            real_two = tmp;
            real_two += real_one;
            imaginary_two += imaginary_one;
            if (((real_two * real_two) + (imaginary_two * imaginary_two)) > 4.0) break;
		} while (it < iterationLimit);
		return it;
	}

}