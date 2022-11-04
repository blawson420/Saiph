#pragma once

struct Vec2f
{
	float x, y;

	// interface
	Vec2f();
	Vec2f(float _x, float _y);
	float Magnitude() const;
	void Normalize();
	void RotateZ(float angle);

	// operators
	Vec2f operator +(const Vec2f&) const;
	Vec2f operator -() const;
	Vec2f operator -(const Vec2f&) const;
	float operator *(const Vec2f&) const;
	Vec2f operator *(float) const;
	Vec2f operator /(float) const;
	const Vec2f& operator +=(const Vec2f&);
	const Vec2f& operator -=(const Vec2f&);
	const Vec2f& operator *=(float);
	const Vec2f& operator /=(float);
};
