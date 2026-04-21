package com.vyatsu.smarthome.repository;

import com.vyatsu.smarthome.entity.SmartDevice;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface DeviceRepository extends JpaRepository<SmartDevice, Long> {
}