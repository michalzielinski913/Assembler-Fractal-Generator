// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <corecrt_math.h>
#define MandelBrot __declspec(dllexport)
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}
extern "C" MandelBrot int countIterations(double firstReal, double firstImaginary, double secondReal, double secondImaginary, int iterationLimit) {
		int it = 0;
		do {
			it++;
			//second Square
			double tmp = (secondReal * secondReal) - (secondImaginary * secondImaginary);
			secondImaginary = 2 * secondReal * secondImaginary;
			secondImaginary = tmp;
			//Add
			firstReal += secondReal;
			firstImaginary += secondImaginary;

			//Magnitude
			double magnitude = sqrt((firstReal * firstReal) + (firstImaginary * firstImaginary));
			if (magnitude > 2.0) break;
		} while (it < iterationLimit);
		return it;	
}
