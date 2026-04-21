package com.vyatsu.smarthome.controller;

import com.vyatsu.smarthome.entity.DeviceLog;
import com.vyatsu.smarthome.entity.Room;
import com.vyatsu.smarthome.entity.SmartCamera;
import com.vyatsu.smarthome.entity.SmartDevice;
import com.vyatsu.smarthome.entity.SmartLight;
import com.vyatsu.smarthome.repository.DeviceLogRepository;
import com.vyatsu.smarthome.repository.DeviceRepository;
import com.vyatsu.smarthome.repository.RoomRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;

@Controller
public class DeviceController {
    
    @Autowired
    private DeviceRepository deviceRepository;
    
    @Autowired
    private RoomRepository roomRepository;
    
    @Autowired
    private DeviceLogRepository logRepository;
    
    // ==================== ГЛАВНАЯ СТРАНИЦА ====================
    
    @GetMapping("/")
    public String index(Model model) {
        model.addAttribute("devices", deviceRepository.findAll());
        model.addAttribute("rooms", roomRepository.findAll());
        return "index";
    }
    
    // ==================== ДОБАВЛЕНИЕ УСТРОЙСТВ ====================
    
    @GetMapping("/light/add")
    public String addLightForm(Model model) {
        model.addAttribute("light", new SmartLight());
        model.addAttribute("rooms", roomRepository.findAll());
        return "add-light";
    }
    
    @PostMapping("/light/add")
    public String addLight(@ModelAttribute SmartLight light) {
        deviceRepository.save(light);
        
        // Лог: добавление нового света
        DeviceLog log = new DeviceLog("CREATE", light, "Создан новый свет: " + light.getName());
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    @GetMapping("/camera/add")
    public String addCameraForm(Model model) {
        model.addAttribute("camera", new SmartCamera());
        model.addAttribute("rooms", roomRepository.findAll());
        return "add-camera";
    }
    
    @PostMapping("/camera/add")
    public String addCamera(@ModelAttribute SmartCamera camera) {
        deviceRepository.save(camera);
        
        // Лог: добавление новой камеры
        DeviceLog log = new DeviceLog("CREATE", camera, "Создана новая камера: " + camera.getName());
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    // ==================== ВКЛЮЧЕНИЕ/ВЫКЛЮЧЕНИЕ ====================
    
    @PostMapping("/{id}/on")
    public String turnOn(@PathVariable Long id) {
        SmartDevice device = deviceRepository.findById(id).orElseThrow();
        device.turnOn();
        deviceRepository.save(device);
        
        // Лог: включение устройства
        DeviceLog log = new DeviceLog("TURN_ON", device, "Устройство включено");
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    @PostMapping("/{id}/off")
    public String turnOff(@PathVariable Long id) {
        SmartDevice device = deviceRepository.findById(id).orElseThrow();
        device.turnOff();
        deviceRepository.save(device);
        
        // Лог: выключение устройства
        DeviceLog log = new DeviceLog("TURN_OFF", device, "Устройство выключено");
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    // ==================== РЕДАКТИРОВАНИЕ УСТРОЙСТВ ====================
    
    @GetMapping("/light/{id}/edit")
    public String editLightForm(@PathVariable Long id, Model model) {
        SmartLight light = (SmartLight) deviceRepository.findById(id).orElseThrow();
        model.addAttribute("light", light);
        model.addAttribute("rooms", roomRepository.findAll());
        return "edit-light";
    }
    
    @PostMapping("/light/{id}/edit")
    public String editLight(@PathVariable Long id, @ModelAttribute SmartLight updatedLight) {
        SmartLight existingLight = (SmartLight) deviceRepository.findById(id).orElseThrow();
        
        String oldName = existingLight.getName();
        String newName = updatedLight.getName();
        
        existingLight.setName(updatedLight.getName());
        existingLight.setPowerConsumption(updatedLight.getPowerConsumption());
        existingLight.setBrightness(updatedLight.getBrightness());
        existingLight.setColorTemperature(updatedLight.getColorTemperature());
        deviceRepository.save(existingLight);
        
        // Лог: редактирование света
        DeviceLog log = new DeviceLog("EDIT", existingLight, 
            "Свет отредактирован. Было: " + oldName + ", стало: " + newName);
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    @GetMapping("/camera/{id}/edit")
    public String editCameraForm(@PathVariable Long id, Model model) {
        SmartCamera camera = (SmartCamera) deviceRepository.findById(id).orElseThrow();
        model.addAttribute("camera", camera);
        model.addAttribute("rooms", roomRepository.findAll());
        return "edit-camera";
    }
    
    @PostMapping("/camera/{id}/edit")
    public String editCamera(@PathVariable Long id, @ModelAttribute SmartCamera updatedCamera) {
        SmartCamera existingCamera = (SmartCamera) deviceRepository.findById(id).orElseThrow();
        
        String oldName = existingCamera.getName();
        String newName = updatedCamera.getName();
        
        existingCamera.setName(updatedCamera.getName());
        existingCamera.setPowerConsumption(updatedCamera.getPowerConsumption());
        existingCamera.setResolution(updatedCamera.getResolution());
        deviceRepository.save(existingCamera);
        
        // Лог: редактирование камеры
        DeviceLog log = new DeviceLog("EDIT", existingCamera, 
            "Камера отредактирована. Было: " + oldName + ", стало: " + newName);
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    // ==================== УДАЛЕНИЕ УСТРОЙСТВА ====================
    
    @PostMapping("/{id}/delete")
    public String delete(@PathVariable Long id) {
        SmartDevice device = deviceRepository.findById(id).orElseThrow();
        String deviceName = device.getName();
        
        // Сначала удаляем все логи, связанные с устройством
        java.util.List<DeviceLog> logs = logRepository.findByDeviceId(id);
        logRepository.deleteAll(logs);
        
        // Затем удаляем устройство
        deviceRepository.deleteById(id);
        
        // Лог (создаем до удаления, чтобы сохранить имя)
        DeviceLog log = new DeviceLog("DELETE", device, "Удалено устройство: " + deviceName);
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    // ==================== СПЕЦИАЛЬНЫЕ МЕТОДЫ ДЛЯ СВЕТА ====================
    
    @PostMapping("/light/{id}/dim")
    public String dimLight(@PathVariable Long id, @RequestParam int brightness) {
        SmartLight light = (SmartLight) deviceRepository.findById(id).orElseThrow();
        int oldBrightness = light.getBrightness();
        light.setBrightness(brightness);
        deviceRepository.save(light);
        
        // Лог: изменение яркости
        DeviceLog log = new DeviceLog("BRIGHTNESS_CHANGE", light, 
            "Яркость изменена с " + oldBrightness + " на " + brightness);
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    // ==================== СПЕЦИАЛЬНЫЕ МЕТОДЫ ДЛЯ КАМЕРЫ ====================
    
    @PostMapping("/camera/{id}/record")
    public String startRecording(@PathVariable Long id) {
        SmartCamera camera = (SmartCamera) deviceRepository.findById(id).orElseThrow();
        camera.startRecording();
        deviceRepository.save(camera);
        
        // Лог: начало записи
        DeviceLog log = new DeviceLog("START_RECORDING", camera, "Начата запись видео");
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    @PostMapping("/camera/{id}/stop")
    public String stopRecording(@PathVariable Long id) {
        SmartCamera camera = (SmartCamera) deviceRepository.findById(id).orElseThrow();
        camera.stopRecording();
        deviceRepository.save(camera);
        
        // Лог: остановка записи
        DeviceLog log = new DeviceLog("STOP_RECORDING", camera, "Остановлена запись видео");
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    // ==================== УПРАВЛЕНИЕ КОМНАТАМИ ====================
    
    @GetMapping("/rooms")
    public String listRooms(Model model) {
        model.addAttribute("rooms", roomRepository.findAll());
        return "rooms";
    }
    
    @GetMapping("/room/add")
    public String addRoomForm(Model model) {
        model.addAttribute("room", new Room());
        return "add-room";
    }
    
    @PostMapping("/room/add")
    public String addRoom(@ModelAttribute Room room) {
        roomRepository.save(room);
        return "redirect:/rooms";
    }
    
    @PostMapping("/{id}/set-room")
    public String setRoom(@PathVariable Long id, @RequestParam Long roomId) {
        SmartDevice device = deviceRepository.findById(id).orElseThrow();
        Room room = roomRepository.findById(roomId).orElseThrow();
        
        String oldRoom = (device.getRoom() != null) ? device.getRoom().getName() : "нет";
        String newRoom = room.getName();
        
        device.setRoom(room);
        deviceRepository.save(device);
        
        // Лог: перемещение устройства
        DeviceLog log = new DeviceLog("MOVE", device, 
            "Устройство перемещено из комнаты '" + oldRoom + "' в комнату '" + newRoom + "'");
        logRepository.save(log);
        
        return "redirect:/";
    }
    
    // ==================== ПРОСМОТР ЛОГОВ ====================
    
    @GetMapping("/{id}/logs")
    public String viewLogs(@PathVariable Long id, Model model) {
        SmartDevice device = deviceRepository.findById(id).orElseThrow();
        model.addAttribute("device", device);
        model.addAttribute("logs", logRepository.findByDeviceIdOrderByTimestampDesc(id));
        return "logs";
    }
}