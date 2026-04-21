package com.vyatsu.smarthome.entity;

import javax.persistence.*;

@Entity
@DiscriminatorValue("CAMERA")
public class SmartCamera extends SmartDevice {
    
    @Column(name = "is_recording")
    private Boolean isRecording = false;
    
    private Integer resolution;
    
    public Boolean getIsRecording() {
        return isRecording;
    }
    
    public void setIsRecording(Boolean isRecording) {
        this.isRecording = isRecording;
    }
    
    public Integer getResolution() {
        return resolution;
    }
    
    public void setResolution(Integer resolution) {
        this.resolution = resolution;
    }
    
    @Override
    public void turnOn() {
        setIsPowered(true);
    }
    
    @Override
    public void turnOff() {
        setIsPowered(false);
        isRecording = false;
    }
    
    @Override
    public String getInfo() {
        String roomName = (getRoom() != null) ? " | 🏠 " + getRoom().getName() : "";
        String recordingStatus = (isRecording != null && isRecording) ? " 🔴 ЗАПИСЬ" : "";
        return "📷 " + getName() + " | " + getPowerConsumption() + " Вт | " + resolution + "p" + roomName + (getIsPowered() ? " ✅ ВКЛ" : " ❌ ВЫКЛ") + recordingStatus;
    }
    
    public void startRecording() {
        if (getIsPowered() != null && getIsPowered()) {
            isRecording = true;
        }
    }
    
    public void stopRecording() {
        isRecording = false;
    }
}