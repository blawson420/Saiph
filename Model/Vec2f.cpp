#include "Vec2f.h"
#include <cmath>

// Vec2f
Vec2f::Vec2f()
{
	x = y = 0;
}
Vec2f::Vec2f(float _x, float _y)
{
	x = _x;
	y = _y;
}
float Vec2f::Magnitude() const
{
	return sqrtf(this->operator *(*this));
}
void Vec2f::Normalize()
{
	float m = Magnitude();
	if (m) *this = this->operator /(m);
}
void Vec2f::RotateZ(float angle)
{
	float temp, cosine = cosf(angle), sine = sinf(angle);
	temp = x;
	x = temp * cosine - y * sine;
	y = temp * sine + y * cosine;
}
Vec2f Vec2f::operator +(const Vec2f& _r) const
{
	return Vec2f(x + _r.x, y + _r.y);
}
Vec2f Vec2f::operator -() const
{
	return Vec2f(-x, -y);
}
Vec2f Vec2f::operator -(const Vec2f& _r) const
{
	return Vec2f(x - _r.x, y - _r.y);
}
float Vec2f::operator *(const Vec2f& _r) const
{
	return x * _r.x + y * _r.y;
}
Vec2f Vec2f::operator *(float _r) const
{
	return Vec2f(x * _r, y * _r);
}
Vec2f Vec2f::operator /(float _r) const
{
	return Vec2f(x / _r, y / _r);
}
const Vec2f& Vec2f::operator +=(const Vec2f& _r)
{
	x += _r.x;
	y += _r.y;
	return *this;
}
const Vec2f& Vec2f::operator -=(const Vec2f& _r)
{
	x -= _r.x;
	y -= _r.y;
	return *this;
}
const Vec2f& Vec2f::operator *=(float _r)
{
	return (*this) = this->operator *(_r);
}
const Vec2f& Vec2f::operator /=(float _r)
{
	return (*this) = this->operator /(_r);
}