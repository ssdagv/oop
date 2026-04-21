package com.vyatsu.smarthome.entity;

import javax.persistence.*;

@Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "device_type")
public abstract class SmartDevice {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    private String name;
    
    @Column(name = "power_consumption")
    private Double powerConsumption;
    
    @Column(name = "is_powered")
    private Boolean isPowered = false;
    
    // Связь с комнатой (многие к одному)
    @ManyToOne
    @JoinColumn(name = "room_id")
    private Room room;
    
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    
    public Double getPowerConsumption() { return powerConsumption; }
    public void setPowerConsumption(Double powerConsumption) { this.powerConsumption = powerConsumption; }
    
    public Boolean getIsPowered() { return isPowered; }
    public void setIsPowered(Boolean isPowered) { this.isPowered = isPowered; }
    
    public Room getRoom() { return room; }
    public void setRoom(Room room) { this.room = room; }
    
    public abstract void turnOn();
    public abstract void turnOff();
    public abstract String getInfo();
}