#ifndef CommandHeader
#define CommandHeader
#include "Ship.h"
class BaseCommand {
	Ship* target;
	float delay;

public:
	//accessors
	Ship* getTarget() { return target; }
	float getDelay() { return delay; };

	//mutators
	void setTarget(Ship* _target) { this->target = _target; };
	void setDelay(float _delay) { this->delay = _delay; };

	virtual void execute() = 0;
};
#endif