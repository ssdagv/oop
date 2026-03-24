using System;

namespace SmartHomeApp.Models
{
    public class SmartLight : SmartDevice
    {
        private int _brightness;
        private string _colorTemp;

        private static readonly string[] ValidColors = new string[]
        {
            "Теплый", "Холодный", "Дневной", "Красный", 
            "Зеленый", "Синий", "Желтый", "Фиолетовый", "Оранжевый"
        };

        public SmartLight(string deviceName, float powerConsumption, int brightness, string colorTemp)
            : base(deviceName, powerConsumption)
        {
            Brightness = brightness;
            ColorTemp = colorTemp;
        }

        public SmartLight(int id, string deviceName, float powerConsumption, bool isPowered, int brightness, string colorTemp)
            : base(id, deviceName, powerConsumption, isPowered)
        {
            Brightness = brightness;
            ColorTemp = colorTemp;
        }

        public int Brightness
        {
            get { return _brightness; }
            set 
            { 
                if (value < 0)
                    _brightness = 0;
                else if (value > 100)
                    _brightness = 100;
                else
                    _brightness = value;
            }
        }

        public string ColorTemp
        {
            get { return _colorTemp; }
            set 
            { 
                if (IsValidColor(value))
                    _colorTemp = value;
                else
                    _colorTemp = "Теплый";
            }
        }

        public static bool IsValidColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
                return false;
                
            foreach (string validColor in ValidColors)
            {
                if (string.Equals(validColor, color, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public override void TurnOn()
        {
            IsPowered = true;
        }

        public override void TurnOff()
        {
            IsPowered = false;
        }

        public override string DisplayInfo()
        {
            string status = IsPowered ? "Включен" : "Выключен";
            return $"Свет: {DeviceName}\nСтатус: {status}\nМощность: {PowerConsumption} Вт\nЯркость: {Brightness}%\nЦвет: {ColorTemp}";
        }

        public void DimSmoothly(int targetBrightness)
        {
            Brightness = targetBrightness;
        }

        public void ChangeColor(string newColor)
        {
            if (IsValidColor(newColor))
                _colorTemp = newColor;
            else
                throw new ArgumentException($"Недопустимый цвет '{newColor}'");
        }
    }
}