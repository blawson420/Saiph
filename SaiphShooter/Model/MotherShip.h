
#include "VelocityCommand.h"
#include "HeadingCommand.h"
#include <vector>

class MotherShip : public Ship {
	std::vector<BaseCommand *> commands;
	std::vector<Ship *> shadows;
	
	void processCommands(float _delta);
	void cleanUpCommands();
public:
	MotherShip(const Ship& _copy);
	~MotherShip();
	void Heartbeat(float _delta);
	void addShadow(Ship* _shadow);
};
