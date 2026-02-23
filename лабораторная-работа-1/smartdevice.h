#ifndef SMARTDEVICE_H
#define SMARTDEVICE_H

#include <string>
using namespace std;

class SmartDevice {
protected:
    string deviceName;
    bool isPowered;
    float powerConsumption;

public:
    SmartDevice(string name, float power);
    
    virtual void turnOn() = 0;
    virtual void turnOff() = 0;
    virtual void displayInfo() = 0;
    
    string getName();
    float getPowerConsumption();
    bool getStatus();
    
    virtual ~SmartDevice() {}
};

#endif
