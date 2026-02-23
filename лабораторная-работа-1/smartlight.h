#ifndef SMARTLIGHT_H
#define SMARTLIGHT_H

#include "smartdevice.h"
#include <string>

class SmartLight : public SmartDevice {
private:
    int brightness;
    string colorTemp;

public:
    //значения по умолчанию
    SmartLight(string name = "Свет", float power = 10, int brightness = 50, string color = "Теплый");
    
    void turnOn() override;
    void turnOff() override;
    void displayInfo() override;
    
    void dimSmoothly(int targetBrightness);
    void changeColor(string newColor);
    
    int getBrightness() { return brightness; }
    string getColorTemp() { return colorTemp; }
};
