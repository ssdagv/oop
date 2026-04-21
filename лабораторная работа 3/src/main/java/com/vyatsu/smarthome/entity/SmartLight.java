package com.vyatsu.smarthome.entity;

import javax.persistence.*;

@Entity
@DiscriminatorValue("LIGHT")
public class SmartLight extends SmartDevice {
    
    private Integer brightness;
    
    @Column(name = "color_temperature")
    private String colorTemperature;
    
    public Integer getBrightness() {
        return brightness;
    }
    
    public void setBrightness(Integer brightness) {
        this.brightness = brightness;
    }
    
    public String getColorTemperature() {
        return colorTemperature;
    }
    
    public void setColorTemperature(String colorTemperature) {
        this.colorTemperature = colorTemperature;
    }
    
    @Override
    public void turnOn() {
        setIsPowered(true);
    }
    
    @Override
    public void turnOff() {
        setIsPowered(false);
    }
    
    @Override
    public String getInfo() {
        String roomName = (getRoom() != null) ? " | 🏠 " + getRoom().getName() : "";
        return "💡 " + getName() + " | " + getPowerConsumption() + " Вт | Яркость: " + brightness + "% | " + colorTemperature + roomName + (getIsPowered() ? " ✅ ВКЛ" : " ❌ ВЫКЛ");
    }
    
    public void dimSmoothly(int targetBrightness) {
        this.brightness = targetBrightness;
    }
    
    public void changeColor(String newColor) {
        this.colorTemperature = newColor;
    }
}