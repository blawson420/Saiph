// DeflectionStrategyA.cpp : Defines the exported functions for the DLL.
//

#include "framework.h"
#include "DeflectionStrategyA.h"

CDeflectionStrategyA::CDeflectionStrategyA()
{
    
}

void CDeflectionStrategyA::Deflect(float& bulletHeading) const {
	//deflect the bullet heading
	bulletHeading = -bulletHeading;
}

void CDeflectionStrategyA::Destroy() {
	delete this;
}

IDeflectionStrategy* CreateStrategy(float& bulletHeading)
{
	return new CDeflectionStrategyA();
}
