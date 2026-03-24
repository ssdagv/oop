using System;
using System.Windows.Forms;
using SmartHomeApp.Models;

namespace SmartHomeApp.Forms
{
    public partial class AddDeviceForm : Form
    {
        public SmartDevice NewDevice { get; private set; }

        private ComboBox cmbType;
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

        public AddDeviceForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Добавить устройство";
            this.ClientSize = new System.Drawing.Size(320, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblType = new Label() { Text = "Тип устройства:", Location = new System.Drawing.Point(12, 15), AutoSize = true };
            cmbType = new ComboBox() { Location = new System.Drawing.Point(140, 12), Width = 150 };
            cmbType.Items.AddRange(new string[] { "Умный свет", "Умная камера" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);

            Label lblName = new Label() { Text = "Название:", Location = new System.Drawing.Point(12, 45), AutoSize = true };
            txtName = new TextBox() { Location = new System.Drawing.Point(140, 42), Width = 150 };

            Label lblPower = new Label() { Text = "Мощность (Вт):", Location = new System.Drawing.Point(12, 75), AutoSize = true };
            txtPower = new TextBox() { Location = new System.Drawing.Point(140, 72), Width = 150, Text = "10" };

            panelLight = new Panel() { Location = new System.Drawing.Point(12, 105), Size = new System.Drawing.Size(280, 100) };
            
            Label lblBrightness = new Label() { Text = "Яркость (0-100):", Location = new System.Drawing.Point(0, 5), AutoSize = true };
            txtBrightness = new TextBox() { Location = new System.Drawing.Point(100, 2), Width = 100, Text = "50" };
            Label lblBrightnessRange = new Label() { Text = "от 0 до 100", Location = new System.Drawing.Point(205, 5), AutoSize = true, ForeColor = System.Drawing.Color.Gray };
            
            Label lblColor = new Label() { Text = "Цвет:", Location = new System.Drawing.Point(0, 35), AutoSize = true };
            cmbColor = new ComboBox() 
            { 
                Location = new System.Drawing.Point(100, 32), 
                Width = 100,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbColor.Items.AddRange(new string[] { "Теплый", "Холодный", "Дневной", "Красный", "Зеленый", "Синий", "Желтый", "Фиолетовый", "Оранжевый" });
            cmbColor.SelectedIndex = 0;
            
            panelLight.Controls.Add(lblBrightness);
            panelLight.Controls.Add(txtBrightness);
            panelLight.Controls.Add(lblBrightnessRange);
            panelLight.Controls.Add(lblColor);
            panelLight.Controls.Add(cmbColor);

            panelCamera = new Panel() { Location = new System.Drawing.Point(12, 105), Size = new System.Drawing.Size(280, 80), Visible = false };
            
            Label lblRecording = new Label() { Text = "Запись:", Location = new System.Drawing.Point(0, 5), AutoSize = true };
            chkRecording = new CheckBox() { Location = new System.Drawing.Point(100, 2), Text = "Включена" };
            
            Label lblResolution = new Label() { Text = "Разрешение (p):", Location = new System.Drawing.Point(0, 35), AutoSize = true };
            txtResolution = new TextBox() { Location = new System.Drawing.Point(100, 32), Width = 100, Text = "720" };
            Label lblResolutionHint = new Label() { Text = "480/720/1080/2160", Location = new System.Drawing.Point(205, 35), AutoSize = true, ForeColor = System.Drawing.Color.Gray };
            
            panelCamera.Controls.Add(lblRecording);
            panelCamera.Controls.Add(chkRecording);
            panelCamera.Controls.Add(lblResolution);
            panelCamera.Controls.Add(txtResolution);
            panelCamera.Controls.Add(lblResolutionHint);

            btnOk = new Button() { Text = "OK", Location = new System.Drawing.Point(140, 330), Width = 75 };
            btnOk.Click += new EventHandler(BtnOk_Click);
            btnCancel = new Button() { Text = "Отмена", Location = new System.Drawing.Point(220, 330), Width = 75 };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            this.Controls.Add(lblType);
            this.Controls.Add(cmbType);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblPower);
            this.Controls.Add(txtPower);
            this.Controls.Add(panelLight);
            this.Controls.Add(panelCamera);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isLight = cmbType.SelectedIndex == 0;
            panelLight.Visible = isLight;
            panelCamera.Visible = !isLight;
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

            if (cmbType.SelectedIndex == 0)
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
            else
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

                if (cmbType.SelectedIndex == 0)
                {
                    int brightness = int.Parse(txtBrightness.Text);
                    string color = cmbColor.SelectedItem.ToString();
                    NewDevice = new SmartLight(name, power, brightness, color);
                }
                else
                {
                    bool isRecording = chkRecording.Checked;
                    int resolution = int.Parse(txtResolution.Text);
                    NewDevice = new SmartCamera(name, power, isRecording, resolution);
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