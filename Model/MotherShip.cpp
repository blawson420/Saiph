#include "MotherShip.h"

//copy constructor
MotherShip::MotherShip(const Ship& _copy) : Ship(_copy) {
}

MotherShip::~MotherShip() {	
	cleanUpCommands();
}

void MotherShip::addShadow(Ship* _shadow) {
	this->shadows.push_back(_shadow);
}

void MotherShip::Heartbeat(float _delta) {
	Ship::Heartbeat(_delta);
	if (this->GetAfterburnerFlag() == true) {
		for (unsigned int i = 0; i < this->shadows.size(); i++) {
			this->shadows[i]->Heartbeat(_delta);

			HeadingCommand* hc = new HeadingCommand();
			hc->setTarget(shadows[i]);
			hc->setDelay(float(i*.05));
			hc->setHeading(this->GetHeading());
			this->commands.push_back(hc);
			//create a VelocityCommand object with the MotherShips velocity
			VelocityCommand* vc = new VelocityCommand();
			vc->setTarget(shadows[i]);
			vc->setDelay(float(i * .05));
			vc->setVelocity(this->GetVelocity());
			this->commands.push_back(vc);

			processCommands(0.001);
			
		}
		
	}
	else {
		cleanUpCommands();
	}
}

void MotherShip::processCommands(float _delta) {
	for (auto it = commands.begin(); it != commands.end();) {
		(*it)->setDelay((*it)->getDelay() - _delta);
		if ((*it)->getDelay() <= 0) {
			(*it)->execute();
			delete *it;
			it = commands.erase(it);
		}
		else {
			++it;
		}
	}
	
}

void MotherShip::cleanUpCommands() {
	commands.clear();
	for (unsigned int i = 0; i < shadows.size(); i++)
	{
		shadows[i]->SetVelocity(0, 0);
		shadows[i]->SetHeading(this->GetHeading());
		shadows[i]->SetPosition(this->GetPosition());
	}
}
