using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartHomeApp.Models;
using SmartHomeApp.Database;

namespace SmartHomeApp.Forms
{
    public partial class MainForm : Form
    {
        private DatabaseManager _db;
        private List<SmartDevice> _devices;
        private ListBox lstDevices;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnTurnOn;
        private Button btnTurnOff;
        private Button btnRefresh;
        private TextBox txtInfo;
        private Button btnDimSmoothly;
        private Button btnChangeColor;
        private Button btnStartRecording;
        private Button btnStopRecording;
        private GroupBox groupLightControls;
        private GroupBox groupCameraControls;

        public MainForm()
        {
            InitializeComponent();
            _db = new DatabaseManager();
            LoadDevices();
        }

        private void InitializeComponent()
        {
            this.lstDevices = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnTurnOn = new System.Windows.Forms.Button();
            this.btnTurnOff = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.groupLightControls = new System.Windows.Forms.GroupBox();
            this.btnDimSmoothly = new System.Windows.Forms.Button();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.groupCameraControls = new System.Windows.Forms.GroupBox();
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lstDevices
            this.lstDevices.Location = new System.Drawing.Point(12, 12);
            this.lstDevices.Size = new System.Drawing.Size(280, 420);
            this.lstDevices.SelectedIndexChanged += new System.EventHandler(this.lstDevices_SelectedIndexChanged);

            // btnAdd
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Location = new System.Drawing.Point(310, 12);
            this.btnAdd.Size = new System.Drawing.Size(140, 35);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.Location = new System.Drawing.Point(310, 55);
            this.btnEdit.Size = new System.Drawing.Size(140, 35);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Location = new System.Drawing.Point(310, 98);
            this.btnDelete.Size = new System.Drawing.Size(140, 35);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnTurnOn
            this.btnTurnOn.Text = "Включить";
            this.btnTurnOn.Location = new System.Drawing.Point(310, 141);
            this.btnTurnOn.Size = new System.Drawing.Size(140, 35);
            this.btnTurnOn.Click += new System.EventHandler(this.btnTurnOn_Click);

            // btnTurnOff
            this.btnTurnOff.Text = "Выключить";
            this.btnTurnOff.Location = new System.Drawing.Point(310, 184);
            this.btnTurnOff.Size = new System.Drawing.Size(140, 35);
            this.btnTurnOff.Click += new System.EventHandler(this.btnTurnOff_Click);

            // btnRefresh
            this.btnRefresh.Text = "Обновить список";
            this.btnRefresh.Location = new System.Drawing.Point(310, 227);
            this.btnRefresh.Size = new System.Drawing.Size(140, 35);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // txtInfo
            this.txtInfo.Location = new System.Drawing.Point(12, 440);
            this.txtInfo.Multiline = true;
            this.txtInfo.Size = new System.Drawing.Size(428, 120);
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Font = new System.Drawing.Font("Consolas", 10);

            // groupLightControls
            this.groupLightControls.Text = "Управление светом";
            this.groupLightControls.Location = new System.Drawing.Point(310, 270);
            this.groupLightControls.Size = new System.Drawing.Size(140, 85);
            this.groupLightControls.Visible = false;

            this.btnDimSmoothly.Text = "Плавно изменить яркость";
            this.btnDimSmoothly.Location = new System.Drawing.Point(6, 20);
            this.btnDimSmoothly.Size = new System.Drawing.Size(118, 28);
            this.btnDimSmoothly.Click += new System.EventHandler(this.btnDimSmoothly_Click);

            this.btnChangeColor.Text = "Изменить цвет";
            this.btnChangeColor.Location = new System.Drawing.Point(6, 52);
            this.btnChangeColor.Size = new System.Drawing.Size(118, 28);
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);

            this.groupLightControls.Controls.Add(this.btnDimSmoothly);
            this.groupLightControls.Controls.Add(this.btnChangeColor);

            // groupCameraControls
            this.groupCameraControls.Text = "Управление камерой";
            this.groupCameraControls.Location = new System.Drawing.Point(310, 270);
            this.groupCameraControls.Size = new System.Drawing.Size(140, 85);
            this.groupCameraControls.Visible = false;

            this.btnStartRecording.Text = "Начать запись";
            this.btnStartRecording.Location = new System.Drawing.Point(6, 20);
            this.btnStartRecording.Size = new System.Drawing.Size(118, 28);
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);

            this.btnStopRecording.Text = "Остановить запись";
            this.btnStopRecording.Location = new System.Drawing.Point(6, 52);
            this.btnStopRecording.Size = new System.Drawing.Size(118, 28);
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);

            this.groupCameraControls.Controls.Add(this.btnStartRecording);
            this.groupCameraControls.Controls.Add(this.btnStopRecording);

            // MainForm
            this.Text = "Умный дом - Система управления устройствами";
            this.ClientSize = new System.Drawing.Size(510, 580);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            this.Controls.Add(this.lstDevices);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnTurnOn);
            this.Controls.Add(this.btnTurnOff);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.groupLightControls);
            this.Controls.Add(this.groupCameraControls);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadDevices()
        {
            _devices = _db.GetAllDevices();
            lstDevices.Items.Clear();
            foreach (var device in _devices)
            {
                lstDevices.Items.Add($"{device.DeviceName} ({(device.IsPowered ? "Вкл" : "Выкл")})");
            }
        }

        private void lstDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                txtInfo.Text = device.DisplayInfo();
                
                if (device is SmartLight)
                {
                    groupLightControls.Visible = true;
                    groupCameraControls.Visible = false;
                }
                else if (device is SmartCamera)
                {
                    groupLightControls.Visible = false;
                    groupCameraControls.Visible = true;
                }
                else
                {
                    groupLightControls.Visible = false;
                    groupCameraControls.Visible = false;
                }
            }
            else
            {
                groupLightControls.Visible = false;
                groupCameraControls.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new AddDeviceForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                _db.AddDevice(addForm.NewDevice);
                LoadDevices();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                var editForm = new EditDeviceForm(device);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    _db.UpdateDevice(editForm.EditedDevice);
                    LoadDevices();
                }
            }
            else
            {
                MessageBox.Show("Выберите устройство для редактирования", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                if (MessageBox.Show($"Удалить устройство '{device.DeviceName}'?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _db.DeleteDevice(device.Id);
                    LoadDevices();
                }
            }
            else
            {
                MessageBox.Show("Выберите устройство для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTurnOn_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                device.TurnOn();
                _db.TogglePower(device.Id, true);
                LoadDevices();
                txtInfo.Text = device.DisplayInfo();
                MessageBox.Show($"Устройство '{device.DeviceName}' включено!", "Успех", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Выберите устройство", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTurnOff_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                device.TurnOff();
                _db.TogglePower(device.Id, false);
                LoadDevices();
                txtInfo.Text = device.DisplayInfo();
                MessageBox.Show($"Устройство '{device.DeviceName}' выключено!", "Успех", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Выберите устройство", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDevices();
            MessageBox.Show("Список устройств обновлен!", "Обновление", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDimSmoothly_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                if (device is SmartLight light)
                {
                    using (var dialog = new Form())
                    {
                        dialog.Text = "Плавное изменение яркости";
                        dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                        dialog.MaximizeBox = false;
                        dialog.MinimizeBox = false;
                        dialog.StartPosition = FormStartPosition.CenterParent;
                        dialog.ClientSize = new System.Drawing.Size(250, 120);
                        
                        Label lbl = new Label() { Text = "Яркость (0-100):", Location = new System.Drawing.Point(12, 15), AutoSize = true };
                        TextBox txt = new TextBox() { Location = new System.Drawing.Point(12, 40), Width = 200, Text = light.Brightness.ToString() };
                        Button btn = new Button() { Text = "OK", Location = new System.Drawing.Point(12, 70), Width = 200 };
                        
                        btn.Click += (s, args) => 
                        {
                            if (int.TryParse(txt.Text, out int newBrightness))
                            {
                                if (newBrightness >= 0 && newBrightness <= 100)
                                {
                                    light.DimSmoothly(newBrightness);
                                    _db.UpdateDevice(light);
                                    LoadDevices();
                                    txtInfo.Text = light.DisplayInfo();
                                    MessageBox.Show($"Яркость плавно изменена на {newBrightness}%", "Успех", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dialog.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Яркость должна быть от 0 до 100!", "Ошибка", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите корректное число!", "Ошибка", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        };
                        
                        dialog.Controls.Add(lbl);
                        dialog.Controls.Add(txt);
                        dialog.Controls.Add(btn);
                        dialog.ShowDialog();
                    }
                }
            }
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                if (device is SmartLight light)
                {
                    using (var dialog = new Form())
                    {
                        dialog.Text = "Изменение цвета";
                        dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                        dialog.MaximizeBox = false;
                        dialog.MinimizeBox = false;
                        dialog.StartPosition = FormStartPosition.CenterParent;
                        dialog.ClientSize = new System.Drawing.Size(280, 160);
                        
                        Label lbl = new Label() { Text = "Выберите цвет:", Location = new System.Drawing.Point(12, 15), AutoSize = true };
                        
                        ComboBox cmbColors = new ComboBox()
                        {
                            Location = new System.Drawing.Point(12, 40),
                            Width = 240,
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        
                        cmbColors.Items.AddRange(new string[] 
                        { 
                            "Теплый", "Холодный", "Дневной", "Красный",
                            "Зеленый", "Синий", "Желтый", "Фиолетовый", "Оранжевый"
                        });
                        
                        if (cmbColors.Items.Contains(light.ColorTemp))
                            cmbColors.SelectedItem = light.ColorTemp;
                        else
                            cmbColors.SelectedIndex = 0;
                        
                        Button btnOk = new Button() { Text = "OK", Location = new System.Drawing.Point(12, 80), Width = 110 };
                        Button btnCancel = new Button() { Text = "Отмена", Location = new System.Drawing.Point(142, 80), Width = 110 };
                        
                        btnOk.Click += (s, args) => 
                        {
                            string selectedColor = cmbColors.SelectedItem.ToString();
                            light.ChangeColor(selectedColor);
                            _db.UpdateDevice(light);
                            LoadDevices();
                            txtInfo.Text = light.DisplayInfo();
                            MessageBox.Show($"Цвет изменен на '{selectedColor}'", "Успех", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dialog.Close();
                        };
                        
                        btnCancel.Click += (s, args) => dialog.Close();
                        
                        dialog.Controls.Add(lbl);
                        dialog.Controls.Add(cmbColors);
                        dialog.Controls.Add(btnOk);
                        dialog.Controls.Add(btnCancel);
                        dialog.ShowDialog();
                    }
                }
            }
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                if (device is SmartCamera camera)
                {
                    if (!camera.IsPowered)
                    {
                        MessageBox.Show("Сначала включите камеру!", "Внимание", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    camera.StartRecording();
                    _db.UpdateDevice(camera);
                    LoadDevices();
                    txtInfo.Text = camera.DisplayInfo();
                    MessageBox.Show("Запись видео начата!", "Успех", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            if (lstDevices.SelectedIndex >= 0 && lstDevices.SelectedIndex < _devices.Count)
            {
                var device = _devices[lstDevices.SelectedIndex];
                if (device is SmartCamera camera)
                {
                    camera.StopRecording();
                    _db.UpdateDevice(camera);
                    LoadDevices();
                    txtInfo.Text = camera.DisplayInfo();
                    MessageBox.Show("Запись видео остановлена!", "Успех", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}