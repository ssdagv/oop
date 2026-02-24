#include <iostream>
#include "smartdevice.h"
#include "smartlight.h"
#include "smartcamera.h"

int main() {

    SmartLight light("светюля");  
    
    SmartCamera cam1("камера", 8, true, 1920);     
    SmartCamera cam2("Уличная камера", false);      
    SmartCamera cam3;                                 
    
    // проверка
    std::cout << "\nСВЕТ:" << std::endl;
    std::cout << "  Имя: " << light.getName() << std::endl;
    std::cout << "  Мощность: " << light.getPowerConsumption() << " Вт" << std::endl;
    std::cout << "  Яркость: " << light.getBrightness() << "%" << std::endl;
    std::cout << "  Цвет: " << light.getColorTemp() << std::endl;
    
    std::cout << "\nКАМЕРА 1:" << std::endl;
    std::cout << "  Имя: " << cam1.getName() << std::endl;
    std::cout << "  Мощность: " << cam1.getPowerConsumption() << " Вт" << std::endl;
    std::cout << "  Запись: " << (cam1.getIsRecording() ? "да" : "нет") << std::endl;
    std::cout << "  Разрешение: " << cam1.getResolution() << "p" << std::endl;
    
    std::cout << "\nКАМЕРА 2:" << std::endl;
    std::cout << "  Имя: " << cam2.getName() << std::endl;
    std::cout << "  Мощность: " << cam2.getPowerConsumption() << " Вт " << std::endl;
    std::cout << "  Запись: " << (cam2.getIsRecording() ? "да" : "нет") << std::endl;
    std::cout << "  Разрешение: " << cam2.getResolution() << "p " << std::endl;
    
    std::cout << "\nКАМЕРА 3 :" << std::endl;
    std::cout << "  Имя: " << cam3.getName() << std::endl;
    std::cout << "  Мощность: " << cam3.getPowerConsumption() << " Вт" << std::endl;
    std::cout << "  Запись: " << (cam3.getIsRecording() ? "да" : "нет") << std::endl;
    std::cout << "  Разрешение: " << cam3.getResolution() << "p" << std::endl;

    std::cout << std::endl;
    
   SmartDevice* devices[] = {&light, &cam1, &cam2, &cam3};
    int deviceCount = 4;
    
    // Включение устройств
    std::cout << "Включение:" << std::endl;
    devices[0]->turnOn();  
    devices[1]->turnOn(); 
    devices[2]->turnOn(); 
    devices[3]->turnOn();
    
    // Информация
    std::cout << "\nИнформация:" << std::endl;
    devices[0]->displayInfo();
    devices[1]->displayInfo();
    devices[2]->displayInfo();
    devices[3]->displayInfo();
    
    // Уникальные методы
    dynamic_cast<SmartLight*>(devices[0])->dimSmoothly(50);
    dynamic_cast<SmartLight*>(devices[0])->changeColor("Холодный");

    dynamic_cast<SmartCamera*>(devices[1])->startRecording();
    dynamic_cast<SmartCamera*>(devices[1])->stopRecording();

    
    // Выключение
    std::cout << "\nВыключение:" << std::endl;
    devices[0]->turnOff();
    devices[1]->turnOff();
    devices[2]->turnOff();
    devices[3]->turnOff();
    
    return 0;
}
