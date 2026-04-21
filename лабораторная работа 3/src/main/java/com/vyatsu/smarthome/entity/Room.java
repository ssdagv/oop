package com.vyatsu.smarthome.entity;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "rooms")
public class Room {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(nullable = false, unique = true)
    private String name;
    
    private Double area;
    
    private String floor;
    
    @OneToMany(mappedBy = "room", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<SmartDevice> devices = new ArrayList<>();
    
    public Room() {}
    
    public Room(String name, Double area, String floor) {
        this.name = name;
        this.area = area;
        this.floor = floor;
    }
    
    // Геттеры и сеттеры
    public Long getId() {
        return id;
    }
    
    public void setId(Long id) {
        this.id = id;
    }
    
    public String getName() {
        return name;
    }
    
    public void setName(String name) {
        this.name = name;
    }
    
    public Double getArea() {
        return area;
    }
    
    public void setArea(Double area) {
        this.area = area;
    }
    
    public String getFloor() {
        return floor;
    }
    
    public void setFloor(String floor) {
        this.floor = floor;
    }
    
    public List<SmartDevice> getDevices() {
        return devices;
    }
    
    public void setDevices(List<SmartDevice> devices) {
        this.devices = devices;
    }
    
    public void addDevice(SmartDevice device) {
        devices.add(device);
        device.setRoom(this);
    }
    
    // Убрали @Override - это не метод переопределения!
    public String getInfo() {
        return "🏠 " + name + " | Этаж: " + floor + " | Площадь: " + area + " м²";
    }
}