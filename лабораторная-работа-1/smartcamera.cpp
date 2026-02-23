#include "smartcamera.h"
#include <iostream>
using namespace std;

SmartCamera::SmartCamera(string name, float power, bool recording, int resolution)
    : SmartDevice(name, power), isRecording(recording), resolution(resolution) 
{
}

void SmartCamera::turnOn() {
    isPowered = true;
    cout << deviceName << " (камера) включена. ";
    if (isRecording) {
        cout << "Запись активна." << endl;
    } else {
        cout << "Запись неактивна." << endl;
    }
}

void SmartCamera::turnOff() {
    isPowered = false;
    cout << deviceName << " (камера) выключена." << endl;
}

void SmartCamera::displayInfo() {
    cout << "Устройство: " << deviceName 
         << " | Тип: Камера"
         << " | Статус: " << (isPowered ? "ВКЛ" : "ВЫКЛ")
         << " | Потребление: " << powerConsumption << " Вт"
         << " | Запись: " << (isRecording ? "ДА" : "НЕТ")
         << " | Разрешение: " << resolution << "p" << endl;
}

void SmartCamera::startRecording() {
    if (!isPowered) {
        cout << deviceName << ": Сначала включите камеру!" << endl;
        return;
    }
    isRecording = true;
    cout << deviceName << ": Запись начата в " << resolution << "p" << endl;
}

void SmartCamera::stopRecording() {
    isRecording = false;
    cout << deviceName << ": Запись остановлена." << endl;
}
