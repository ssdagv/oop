#ifndef SMARTCAMERA_H
#define SMARTCAMERA_H

#include "smartdevice.h"

class SmartCamera : public SmartDevice {
private:
    bool isRecording;
    int resolution;

public:
    //значения по умолчанию
    SmartCamera(string name = "Камера", float power = 5, bool recording = false, int resolution = 720);
    
    void turnOn() override;
    void turnOff() override;
    void displayInfo() override;
    
    void startRecording();
    void stopRecording();
    
    bool getIsRecording() { return isRecording; }
    int getResolution() { return resolution; }
};

#endif
