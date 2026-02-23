#include "smartlight.h"
#include <iostream>
using namespace std;

SmartLight::SmartLight(string name, float power, int brightness, string color)
    : SmartDevice(name, power), brightness(brightness), colorTemp(color) 
{
}

void SmartLight::turnOn() {
    isPowered = true;
    cout << deviceName << " (свет) включен. Яркость: " << brightness 
         << "%, Цвет: " << colorTemp << endl;
}

void SmartLight::turnOff() {
    isPowered = false;
    cout << deviceName << " (свет) выключен." << endl;
}

void SmartLight::displayInfo() {
    cout << "Устройство: " << deviceName 
         << " | Тип: Свет"
         << " | Статус: " << (isPowered ? "ВКЛ" : "ВЫКЛ")
         << " | Потребление: " << powerConsumption << " Вт"
         << " | Яркость: " << brightness << "%"
         << " | Цвет: " << colorTemp << endl;
}

void SmartLight::dimSmoothly(int targetBrightness) {
    if (!isPowered) {
        cout << deviceName << ": Сначала включите свет!" << endl;
        return;
    }
    cout << deviceName << ": Плавное изменение яркости с " << brightness 
         << "% до " << targetBrightness << "%" << endl;
    brightness = targetBrightness;
    cout << "  Яркость установлена: " << brightness << "%" << endl;
}

void SmartLight::changeColor(string newColor) {
    cout << deviceName << ": Изменение цвета с " 
         << colorTemp << " на " << newColor << endl;
    colorTemp = newColor;
}
