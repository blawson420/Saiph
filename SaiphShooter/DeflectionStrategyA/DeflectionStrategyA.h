// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the DEFLECTIONSTRATEGYA_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// DEFLECTIONSTRATEGYA_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef DEFLECTIONSTRATEGYA_EXPORTS
#define DEFLECTIONSTRATEGYA_API __declspec(dllexport)
#else
#define DEFLECTIONSTRATEGYA_API __declspec(dllimport)
#endif
#include "../Model/IDeflectionStrategy.h"

// This class is exported from the dll
class CDeflectionStrategyA : public IDeflectionStrategy {
public:
	CDeflectionStrategyA(void);
	// TODO: add your methods here.
	void Deflect(float& bulletHeading) const;
	void Destroy();
};


extern "C" DEFLECTIONSTRATEGYA_API IDeflectionStrategy* CreateStrategy(float& bulletHeading);
