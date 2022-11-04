// DeflectionStrategyB.cpp : Defines the exported functions for the DLL.
//

#include "framework.h"
#include "DeflectionStrategyB.h"


CDeflectionStrategyB::CDeflectionStrategyB()
{

}

void CDeflectionStrategyB::Deflect(float& bulletHeading) const {
	//deflect the bullet heading
	bulletHeading *= 15;
}

void CDeflectionStrategyB::Destroy() {
	delete this;
}

IDeflectionStrategy* CreateStrategy(float& bulletHeading)
{
	return new CDeflectionStrategyB();
}
