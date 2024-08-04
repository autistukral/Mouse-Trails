using System;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading.Channels;

namespace Mouse_Trails
{
    public partial class MouseTrailsForm : Form
    {
        private System.Windows.Forms.Timer updateTimer;
        private bool isAutoUpdateEnabled;

        // Code to make the application top bar colored by the windows
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        const uint SPI_GETMOUSETRAILS = 0x005E;
        const uint SPI_SETMOUSE = 0x0004;
        const uint SPI_SETMOUSETRAILS = 0x005D;
        const uint SPIF_UPDATEINIFILE = 0x01;
        const uint SPIF_SENDCHANGE = 0x02;

        private const string appName = "Mouse Trails";

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);

        public MouseTrailsForm()
        {
            InitializeComponent();
            trailsCountCB.SelectedIndex = 3;
            updateRateCB.SelectedIndex = 2;
            CustomComboBox();

            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = GetUpdateTimerFromRegistry(); // Set the interval to X ms
            updateTimer.Tick += UpdateTimer_Tick;
        }

        private int UpdateCBValue()
        {
            if (updateRateCB.SelectedIndex == 0)
            {
                return 1000;
            }
            else if (updateRateCB.SelectedIndex == 1)
            {
                return 2000;
            }
            else if (updateRateCB.SelectedIndex == 2)
            {
                return 5000;
            }
            else if (updateRateCB.SelectedIndex == 3)
            {
                return 10000;
            }
            else if (updateRateCB.SelectedIndex == 4)
            {
                return 30000;
            }
            return 5000;
        }

        private int GetUpdateCBValue()
        {
            if (GetUpdateTimerFromRegistry() == 1000)
            {
                return 0;
            }
            else if (GetUpdateTimerFromRegistry() == 2000)
            {
                return 1;
            }
            else if (GetUpdateTimerFromRegistry() == 5000)
            {
                return 2;
            }
            else if (GetUpdateTimerFromRegistry() == 10000)
            {
                return 3;
            }
            else if (GetUpdateTimerFromRegistry() == 30000)
            {
                return 4;
            }
            return 0;
        }

        private void CustomComboBox()
        {
            // Set the DrawMode to OwnerDrawFixed
            trailsCountCB.DrawMode = DrawMode.OwnerDrawFixed;
            trailsCountCB.DrawItem += new DrawItemEventHandler(trailsCountCB_DrawItem);

            updateRateCB.DrawMode = DrawMode.OwnerDrawFixed;
            updateRateCB.DrawItem += new DrawItemEventHandler(trailsCountCB_DrawItem);
        }

        private void updateRateCB_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void trailsCountCB_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Check if the item index is valid
            if (e.Index < 0) return;

            // Get the ComboBox control
            System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;

            // Get the item to be drawn
            string item = comboBox.Items[e.Index].ToString();

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(comboBox.BackColor), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(comboBox.BackColor), e.Bounds);
            }

            // Set the text color for the item
            e.Graphics.DrawString(item, e.Font, new SolidBrush(comboBox.ForeColor), e.Bounds);

            // Draw the focus rectangle if the item has focus
            e.DrawFocusRectangle();
        }

        private void MouseTrailsForm_Load(object sender, EventArgs e)
        {

            // Check if the app is set to run at startup
            runOnStartupToolStripMenuItem.Checked = IsRunAtStartup();
            notifyIcon.Visible = true;

            int savedTrailCount = GetTrailCountFromRegistry();
            trailsCountCB.SelectedItem = savedTrailCount.ToString();
            int savedUpdateCount = GetUpdateCBValue();
            updateRateCB.SelectedIndex = savedUpdateCount;

            // Minimize if started by Windows
            if (IsRunAtStartup() != null)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Hide();
                notifyIcon.Visible = true;
            }

            // Load the toggle state from the registry
            isAutoUpdateEnabled = GetAutoApplyFromRegistry();
            AutoApplyToggle();

            UpdateTrayText();
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateTrayText();
            AutoApplyToggle();
            if (isAutoUpdateEnabled)
            {
                int trailsFromRegistry = GetTrailCountFromRegistry();
                int currentTrails = GetMouseTrails();
                if (trailsFromRegistry != currentTrails)
                {
                    SetMouseTrails(trailsFromRegistry);
                }
            }
        }

        private void applyTrails_Click(object sender, EventArgs e)
        {
            int selectedValue = int.Parse(trailsCountCB.SelectedItem.ToString());
            int updateValue1 = UpdateCBValue();
            SetMouseTrails(selectedValue);
            SaveTrailCountRegistry(selectedValue);
            SaveUpdateTimerRegistry(updateValue1);
            UpdateTrayText();
        }

        private void autoApplyButton_Click(object sender, EventArgs e)
        {
            int selectedValue = int.Parse(trailsCountCB.SelectedItem.ToString());
            int updateValue1 = UpdateCBValue();
            SaveTrailCountRegistry(selectedValue);
            SaveUpdateTimerRegistry(updateValue1);
            isAutoUpdateEnabled = !isAutoUpdateEnabled;
            SaveAutoApplyRegistry(isAutoUpdateEnabled);
            AutoApplyToggle();
        }

        private void AutoApplyToggle()
        {
            autoApplyButton.Text = isAutoUpdateEnabled ? "Disable\nAuto Update" : "Enable\nAuto Update";
            autoApplyButton.BackColor = isAutoUpdateEnabled ? Color.FromArgb(160,0,40) : Color.FromArgb(0,160,40);
        }

        private void SaveAutoApplyRegistry(bool state)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\MouseTrails\Settings");
                if (key != null)
                {
                    key.SetValue("AutoApply", state ? 1 : 0, RegistryValueKind.DWord);
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool GetAutoApplyFromRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MouseTrails\Settings");
                if (key != null)
                {
                    object value = key.GetValue("AutoApply");
                    key.Close();

                    if (value != null && int.TryParse(value.ToString(), out int state))
                    {
                        return state == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false; // Default value if not found or error occurred
        }

        private void SaveTrailCountRegistry(int count)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\MouseTrails\Settings");
                if (key != null)
                {
                    key.SetValue("TrailCount", count, RegistryValueKind.DWord);
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetTrailCountFromRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MouseTrails\Settings");
                if (key != null)
                {
                    object value = key.GetValue("TrailCount");
                    key.Close();

                    if (value != null && int.TryParse(value.ToString(), out int count))
                    {
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0; // Default value if not found or error occurred
        }

        private void SaveUpdateTimerRegistry(int count)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\MouseTrails\Settings");
                if (key != null)
                {
                    key.SetValue("UpdateTimer", count, RegistryValueKind.DWord);
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetUpdateTimerFromRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MouseTrails\Settings");
                if (key != null)
                {
                    object value = key.GetValue("UpdateTimer");
                    key.Close();

                    if (value != null && int.TryParse(value.ToString(), out int count))
                    {
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 5000; // Default value if not found or error occurred
        }

        private void SetMouseTrails(int count)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Mouse", true);
                if (key != null)
                {
                    key.SetValue("MouseTrails", count.ToString(), RegistryValueKind.String);
                    key.Close();

                    // Use SystemParametersInfo to update the mouse trails setting
                    SystemParametersInfo(SPI_SETMOUSETRAILS, (uint)count, IntPtr.Zero, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTrayText()
        {
            int currentValue = GetMouseTrails();
            if (currentValue > 0)
            {

                notifyIcon.Text = $"{appName}: {currentValue}";
                currentTrailsUpdateLabel.Text = currentValue.ToString();
            }
            else
            {
                notifyIcon.Text = $"{appName}: Disabled";
                currentTrailsUpdateLabel.Text = "Disabled";
            }

        }

        private int GetMouseTrails()
        {
            int trails = 0;
            try
            {
                SystemParametersInfo(SPI_GETMOUSETRAILS, 0, ref trails, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return trails;
        }

        private void MouseTrailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Cancel the form closing event
            this.Hide(); // Hide the form
            notifyIcon.Visible = true; // Show the NotifyIcon
        }

        private void notifyIcon_MouseDoubleClick(object sender, EventArgs e)
        {
            this.Show(); // Show the form
            this.WindowState = FormWindowState.Normal; // Restore the window state
            notifyIcon.Visible = true; // Hide the NotifyIcon
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = true; // Hide the NotifyIcon
            Application.Exit(); // Exit the application
            Application.ExitThread();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = true;
        }

        private void runOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Toggle the checked state
            runOnStartupToolStripMenuItem.Checked = !runOnStartupToolStripMenuItem.Checked;

            if (runOnStartupToolStripMenuItem.Checked)
            {
                AddToStartup();
            }
            else
            {
                RemoveFromStartup();
            }
        }

        private void AddToStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue(appName, Application.ExecutablePath);
            key.Close();
        }

        private void RemoveFromStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.DeleteValue(appName, false);
            key.Close();
        }

        private bool IsRunAtStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            return key.GetValue(appName) != null;
        }

    }
}
