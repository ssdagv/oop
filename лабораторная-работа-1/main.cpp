#include <iostream>
#include "smartdevice.h"
#include "smartlight.h"
#include "smartcamera.h"

int main() {

    SmartLight light;  
    SmartCamera camera("Моя камера"); 
    
    // проверка  
    std::cout << "\nСВЕТ:" << std::endl;
    std::cout << "  Имя: " << light.getName() << std::endl;
    std::cout << "  Мощность: " << light.getPowerConsumption() << " Вт" << std::endl;
    std::cout << "  Яркость: " << light.getBrightness() << "%" << std::endl;
    std::cout << "  Цвет: " << light.getColorTemp() << std::endl;
    
    std::cout << "\nКАМЕРА:" << std::endl;
    std::cout << "  Имя: " << camera.getName() << std::endl;
    std::cout << "  Мощность: " << camera.getPowerConsumption() << " Вт" << std::endl;
    std::cout << "  Запись: " << (camera.getIsRecording() ? "да" : "нет") << std::endl;
    std::cout << "  Разрешение: " << camera.getResolution() << "p" << std::endl;

     std::cout << std::endl;
    

    SmartDevice* devices[] = {&light, &camera};
    
    // Включение устройств
    std::cout << "Включение:" << std::endl;
    devices[0]->turnOn();  // Свет
    devices[1]->turnOn();  // Камера
    
    // Информация
    std::cout << "\nИнформация:" << std::endl;
    devices[0]->displayInfo();
    devices[1]->displayInfo();
    
    // Уникальные методы
    std::cout << "\nУникальные методы:" << std::endl;
    
    // Для света
    SmartLight* lightPtr = dynamic_cast<SmartLight*>(devices[0]);
    lightPtr->dimSmoothly(50);
    lightPtr->changeColor("Холодный");
    
    // Для камеры
    SmartCamera* cameraPtr = dynamic_cast<SmartCamera*>(devices[1]);
    cameraPtr->startRecording();
    cameraPtr->stopRecording();
    
    // Выключение
    std::cout << "\nВыключение:" << std::endl;
    devices[0]->turnOff();
    devices[1]->turnOff();
    
    return 0;
}
