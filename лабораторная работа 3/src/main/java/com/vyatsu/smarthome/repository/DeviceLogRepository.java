package com.vyatsu.smarthome.repository;

import com.vyatsu.smarthome.entity.DeviceLog;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import java.util.List;

@Repository
public interface DeviceLogRepository extends JpaRepository<DeviceLog, Long> {
    List<DeviceLog> findByDeviceId(Long deviceId);
    
    // Добавь этот метод для сортировки по времени (сначала новые)
    List<DeviceLog> findByDeviceIdOrderByTimestampDesc(Long deviceId);
}