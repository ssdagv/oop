using System;
using System.Collections.Generic;
using Npgsql;
using SmartHomeApp.Models;

namespace SmartHomeApp.Database
{
    public class DatabaseManager
    {
        private string _connectionString;

        public DatabaseManager()
        {
            _connectionString = "Host=localhost;Database=smarthome;Username=postgres;Password=postgres";
        }

        public List<SmartDevice> GetAllDevices()
        {
            List<SmartDevice> devices = new List<SmartDevice>();

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM devices ORDER BY id";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader["device_type"]?.ToString() ?? "";
                            int id = Convert.ToInt32(reader["id"]);
                            string name = reader["device_name"]?.ToString() ?? "Неизвестно";
                            float power = Convert.ToSingle(reader["power_consumption"]);
                            bool isPowered = Convert.ToBoolean(reader["is_powered"]);

                            if (type == "Light")
                            {
                                int brightness = reader["brightness"] != DBNull.Value ? Convert.ToInt32(reader["brightness"]) : 50;
                                string color = reader["color_temp"] != DBNull.Value ? reader["color_temp"].ToString() ?? "Теплый" : "Теплый";
                                devices.Add(new SmartLight(id, name, power, isPowered, brightness, color));
                            }
                            else if (type == "Camera")
                            {
                                bool isRecording = reader["is_recording"] != DBNull.Value && Convert.ToBoolean(reader["is_recording"]);
                                int resolution = reader["resolution"] != DBNull.Value ? Convert.ToInt32(reader["resolution"]) : 720;
                                devices.Add(new SmartCamera(id, name, power, isPowered, isRecording, resolution));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка БД: {ex.Message}", "Ошибка", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return devices;
        }

        public void AddDevice(SmartDevice device)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                if (device is SmartLight light)
                {
                    string sql = @"INSERT INTO devices (device_type, device_name, power_consumption, is_powered, brightness, color_temp)
                                   VALUES (@type, @name, @power, @powered, @brightness, @color)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", "Light");
                        cmd.Parameters.AddWithValue("@name", light.DeviceName);
                        cmd.Parameters.AddWithValue("@power", light.PowerConsumption);
                        cmd.Parameters.AddWithValue("@powered", light.IsPowered);
                        cmd.Parameters.AddWithValue("@brightness", light.Brightness);
                        cmd.Parameters.AddWithValue("@color", light.ColorTemp);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (device is SmartCamera camera)
                {
                    string sql = @"INSERT INTO devices (device_type, device_name, power_consumption, is_powered, is_recording, resolution)
                                   VALUES (@type, @name, @power, @powered, @recording, @resolution)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", "Camera");
                        cmd.Parameters.AddWithValue("@name", camera.DeviceName);
                        cmd.Parameters.AddWithValue("@power", camera.PowerConsumption);
                        cmd.Parameters.AddWithValue("@powered", camera.IsPowered);
                        cmd.Parameters.AddWithValue("@recording", camera.IsRecording);
                        cmd.Parameters.AddWithValue("@resolution", camera.Resolution);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UpdateDevice(SmartDevice device)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                if (device is SmartLight light)
                {
                    string sql = @"UPDATE devices SET device_name = @name, power_consumption = @power, 
                                   is_powered = @powered, brightness = @brightness, color_temp = @color
                                   WHERE id = @id";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", light.Id);
                        cmd.Parameters.AddWithValue("@name", light.DeviceName);
                        cmd.Parameters.AddWithValue("@power", light.PowerConsumption);
                        cmd.Parameters.AddWithValue("@powered", light.IsPowered);
                        cmd.Parameters.AddWithValue("@brightness", light.Brightness);
                        cmd.Parameters.AddWithValue("@color", light.ColorTemp);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (device is SmartCamera camera)
                {
                    string sql = @"UPDATE devices SET device_name = @name, power_consumption = @power,
                                   is_powered = @powered, is_recording = @recording, resolution = @resolution
                                   WHERE id = @id";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", camera.Id);
                        cmd.Parameters.AddWithValue("@name", camera.DeviceName);
                        cmd.Parameters.AddWithValue("@power", camera.PowerConsumption);
                        cmd.Parameters.AddWithValue("@powered", camera.IsPowered);
                        cmd.Parameters.AddWithValue("@recording", camera.IsRecording);
                        cmd.Parameters.AddWithValue("@resolution", camera.Resolution);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeleteDevice(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM devices WHERE id = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void TogglePower(int id, bool isPowered)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "UPDATE devices SET is_powered = @powered WHERE id = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@powered", isPowered);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}