using System;
using System.Windows.Forms;
using SmartHomeApp.Models;

namespace SmartHomeApp.Forms
{
    public partial class EditDeviceForm : Form
    {
        public SmartDevice EditedDevice { get; private set; }
        private SmartDevice _originalDevice;

        private TextBox txtName;
        private TextBox txtPower;
        private TextBox txtBrightness;
        private ComboBox cmbColor;
        private CheckBox chkRecording;
        private TextBox txtResolution;
        private Button btnOk;
        private Button btnCancel;
        private Panel panelLight;
        private Panel panelCamera;

        public EditDeviceForm(SmartDevice device)
        {
            _originalDevice = device;
            InitializeComponent();
            LoadDeviceData();
        }

        private void InitializeComponent()
        {
            this.Text = "Редактировать устройство";
            this.ClientSize = new System.Drawing.Size(320, 320);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblName = new Label() { Text = "Название:", Location = new System.Drawing.Point(12, 15), AutoSize = true };
            txtName = new TextBox() { Location = new System.Drawing.Point(120, 12), Width = 170 };

            Label lblPower = new Label() { Text = "Мощность (Вт):", Location = new System.Drawing.Point(12, 45), AutoSize = true };
            txtPower = new TextBox() { Location = new System.Drawing.Point(120, 42), Width = 170 };

            panelLight = new Panel() { Location = new System.Drawing.Point(12, 75), Size = new System.Drawing.Size(280, 80) };
            
            Label lblBrightness = new Label() { Text = "Яркость (0-100):", Location = new System.Drawing.Point(0, 5), AutoSize = true };
            txtBrightness = new TextBox() { Location = new System.Drawing.Point(100, 2), Width = 100 };
            Label lblBrightnessRange = new Label() { Text = "от 0 до 100", Location = new System.Drawing.Point(205, 5), AutoSize = true, ForeColor = System.Drawing.Color.Gray };
            
            Label lblColor = new Label() { Text = "Цвет:", Location = new System.Drawing.Point(0, 35), AutoSize = true };
            cmbColor = new ComboBox() 
            { 
                Location = new System.Drawing.Point(100, 32), 
                Width = 100,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbColor.Items.AddRange(new string[] { "Теплый", "Холодный", "Дневной", "Красный", "Зеленый", "Синий", "Желтый", "Фиолетовый", "Оранжевый" });
            
            panelLight.Controls.Add(lblBrightness);
            panelLight.Controls.Add(txtBrightness);
            panelLight.Controls.Add(lblBrightnessRange);
            panelLight.Controls.Add(lblColor);
            panelLight.Controls.Add(cmbColor);

            panelCamera = new Panel() { Location = new System.Drawing.Point(12, 75), Size = new System.Drawing.Size(280, 80), Visible = false };
            
            Label lblRecording = new Label() { Text = "Запись:", Location = new System.Drawing.Point(0, 5), AutoSize = true };
            chkRecording = new CheckBox() { Location = new System.Drawing.Point(100, 2), Text = "Включена" };
            
            Label lblResolution = new Label() { Text = "Разрешение (p):", Location = new System.Drawing.Point(0, 35), AutoSize = true };
            txtResolution = new TextBox() { Location = new System.Drawing.Point(100, 32), Width = 100 };
            Label lblResolutionHint = new Label() { Text = "480/720/1080/2160", Location = new System.Drawing.Point(205, 35), AutoSize = true, ForeColor = System.Drawing.Color.Gray };
            
            panelCamera.Controls.Add(lblRecording);
            panelCamera.Controls.Add(chkRecording);
            panelCamera.Controls.Add(lblResolution);
            panelCamera.Controls.Add(txtResolution);
            panelCamera.Controls.Add(lblResolutionHint);

            btnOk = new Button() { Text = "Сохранить", Location = new System.Drawing.Point(120, 250), Width = 85 };
            btnOk.Click += new EventHandler(BtnOk_Click);
            btnCancel = new Button() { Text = "Отмена", Location = new System.Drawing.Point(210, 250), Width = 85 };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblPower);
            this.Controls.Add(txtPower);
            this.Controls.Add(panelLight);
            this.Controls.Add(panelCamera);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
        }

        private void LoadDeviceData()
        {
            txtName.Text = _originalDevice.DeviceName;
            txtPower.Text = _originalDevice.PowerConsumption.ToString();

            if (_originalDevice is SmartLight light)
            {
                panelLight.Visible = true;
                panelCamera.Visible = false;
                txtBrightness.Text = light.Brightness.ToString();
                
                if (cmbColor.Items.Contains(light.ColorTemp))
                    cmbColor.SelectedItem = light.ColorTemp;
                else
                    cmbColor.SelectedIndex = 0;
            }
            else if (_originalDevice is SmartCamera camera)
            {
                panelLight.Visible = false;
                panelCamera.Visible = true;
                chkRecording.Checked = camera.IsRecording;
                txtResolution.Text = camera.Resolution.ToString();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название устройства!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!float.TryParse(txtPower.Text, out float power))
            {
                MessageBox.Show("Мощность должна быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            if (power < 0 || power > 1000)
            {
                MessageBox.Show("Мощность должна быть от 0 до 1000 Вт!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_originalDevice is SmartLight)
            {
                if (!int.TryParse(txtBrightness.Text, out int brightness))
                {
                    MessageBox.Show("Яркость должна быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                if (brightness < 0 || brightness > 100)
                {
                    MessageBox.Show("Яркость должна быть от 0 до 100!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (_originalDevice is SmartCamera)
            {
                if (!int.TryParse(txtResolution.Text, out int resolution))
                {
                    MessageBox.Show("Разрешение должно быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                if (resolution < 0)
                {
                    MessageBox.Show("Разрешение не может быть отрицательным!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                string name = txtName.Text;
                float power = float.Parse(txtPower.Text);

                if (_originalDevice is SmartLight)
                {
                    int brightness = int.Parse(txtBrightness.Text);
                    string color = cmbColor.SelectedItem.ToString();
                    EditedDevice = new SmartLight(_originalDevice.Id, name, power, _originalDevice.IsPowered, brightness, color);
                }
                else if (_originalDevice is SmartCamera)
                {
                    bool isRecording = chkRecording.Checked;
                    int resolution = int.Parse(txtResolution.Text);
                    EditedDevice = new SmartCamera(_originalDevice.Id, name, power, _originalDevice.IsPowered, isRecording, resolution);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}