#include "BaseCommand.h"

class HeadingCommand : public BaseCommand {
	float heading;

public:
	//accessors
	float const getHeading() { return heading; }
	void setHeading(float _heading) { this->heading = _heading; }
	//override execute in baseCommand to set the heading of the target ship
	void BaseCommand::execute() {
		getTarget()->SetHeading(heading);
	}
};