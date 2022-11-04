#include "BaseCommand.h"

class VelocityCommand : public BaseCommand {
	Vec2f velocity;

public:
	//accessors
	Vec2f const getVelocity() { return velocity; }
	void setVelocity(Vec2f _velocity) { this->velocity = _velocity; }
	//override execute in baseCommand to set the heading of the target ship
	void execute() {
		getTarget()->SetVelocity(velocity);
	}
};