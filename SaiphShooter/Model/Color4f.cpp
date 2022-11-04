#include "Color4f.h"

// Color4f
Color4f::Color4f()
{
	a = r = g = b = 1;
}
Color4f::Color4f(float _a, float _r, float _g, float _b)
{
	a = _a;
	r = _r;
	g = _g;
	b = _b;
}