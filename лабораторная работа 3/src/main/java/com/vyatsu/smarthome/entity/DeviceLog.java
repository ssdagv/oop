package com.vyatsu.smarthome.entity;

import javax.persistence.*;
import java.time.LocalDateTime;

@Entity
@Table(name = "device_logs")
public class DeviceLog {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(nullable = false)
    private String action;  // "TURN_ON", "TURN_OFF", "BRIGHTNESS_CHANGE", "START_RECORDING"
    
    @Column(nullable = false)
    private LocalDateTime timestamp;
    
    private String details;
    
    // Связь "многие к одному" с устройством
    @ManyToOne
    @JoinColumn(name = "device_id", nullable = false)
    private SmartDevice device;
    
    // Конструкторы
    public DeviceLog() {}
    
    public DeviceLog(String action, SmartDevice device, String details) {
        this.action = action;
        this.device = device;
        this.details = details;
        this.timestamp = LocalDateTime.now();
    }
    
    // Геттеры и сеттеры
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    
    public String getAction() { return action; }
    public void setAction(String action) { this.action = action; }
    
    public LocalDateTime getTimestamp() { return timestamp; }
    public void setTimestamp(LocalDateTime timestamp) { this.timestamp = timestamp; }
    
    public String getDetails() { return details; }
    public void setDetails(String details) { this.details = details; }
    
    public SmartDevice getDevice() { return device; }
    public void setDevice(SmartDevice device) { this.device = device; }
    
    public String getInfo() {
        return "📝 " + timestamp + " | " + device.getName() + " | " + action + " | " + details;
    }
}