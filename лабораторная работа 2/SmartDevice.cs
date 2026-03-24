using System;

namespace SmartHomeApp.Models
{
    public abstract class SmartDevice
    {
        private int _id;
        private string _deviceName;
        private bool _isPowered;
        private float _powerConsumption;

        protected SmartDevice(string deviceName, float powerConsumption)
        {
            DeviceName = deviceName;
            PowerConsumption = powerConsumption;
            _isPowered = false;
        }

        protected SmartDevice(int id, string deviceName, float powerConsumption, bool isPowered)
        {
            _id = id;
            DeviceName = deviceName;
            PowerConsumption = powerConsumption;
            _isPowered = isPowered;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    _deviceName = "Неизвестное устройство";
                else
                    _deviceName = value;
            }
        }

        public bool IsPowered
        {
            get { return _isPowered; }
            set { _isPowered = value; }
        }

        public float PowerConsumption
        {
            get { return _powerConsumption; }
            set 
            { 
                if (value < 0)
                    _powerConsumption = 0;
                else if (value > 1000)
                    _powerConsumption = 1000;
                else
                    _powerConsumption = value;
            }
        }

        public abstract void TurnOn();
        public abstract void TurnOff();
        public abstract string DisplayInfo();
    }
}