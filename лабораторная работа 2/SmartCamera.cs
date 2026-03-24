using System;

namespace SmartHomeApp.Models
{
    public class SmartCamera : SmartDevice
    {
        private bool _isRecording;
        private int _resolution;

        public SmartCamera(string deviceName, float powerConsumption, bool isRecording, int resolution)
            : base(deviceName, powerConsumption)
        {
            _isRecording = isRecording;
            Resolution = resolution;
        }

        public SmartCamera(int id, string deviceName, float powerConsumption, bool isPowered, bool isRecording, int resolution)
            : base(id, deviceName, powerConsumption, isPowered)
        {
            _isRecording = isRecording;
            Resolution = resolution;
        }

        public bool IsRecording
        {
            get { return _isRecording; }
            set { _isRecording = value; }
        }

        public int Resolution
        {
            get { return _resolution; }
            set 
            { 
                if (value <= 480)
                    _resolution = 480;
                else if (value <= 720)
                    _resolution = 720;
                else if (value <= 1080)
                    _resolution = 1080;
                else
                    _resolution = 2160;
            }
        }

        public override void TurnOn()
        {
            IsPowered = true;
        }

        public override void TurnOff()
        {
            IsPowered = false;
            _isRecording = false;
        }

        public override string DisplayInfo()
        {
            string status = IsPowered ? "Включен" : "Выключен";
            string recordStatus = _isRecording ? "Идет запись" : "Запись не ведется";
            return $"Камера: {DeviceName}\nСтатус: {status}\nМощность: {PowerConsumption} Вт\nЗапись: {recordStatus}\nРазрешение: {_resolution}p";
        }

        public void StartRecording()
        {
            if (IsPowered)
            {
                _isRecording = true;
            }
        }

        public void StopRecording()
        {
            _isRecording = false;
        }
    }
}