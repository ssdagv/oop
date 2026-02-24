#ifndef SMARTCAMERA_H
#define SMARTCAMERA_H

#include "smartdevice.h"

class SmartCamera : public SmartDevice {
private:
    bool isRecording;
    int resolution;

public:
    SmartCamera(string name, float power, bool recording, int resolution);
    SmartCamera(string name, bool recording);
    SmartCamera();
    
    void turnOn() override;
    void turnOff() override;
    void displayInfo() override;
    
    void startRecording();
    void stopRecording();
    
    bool getIsRecording() { return isRecording; }
    int getResolution() { return resolution; }
};

#endif
