#include "smartdevice.h"
#include <iostream>
using namespace std;

SmartDevice::SmartDevice(string name, float power) 
    : deviceName(name), isPowered(false), powerConsumption(power) {}

string SmartDevice::getName() { 
    return deviceName; }
float SmartDevice::getPowerConsumption() { 
    return powerConsumption; }
bool SmartDevice::getStatus() { 
    return isPowered; 
}
