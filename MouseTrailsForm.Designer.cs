namespace Mouse_Trails
{
    partial class MouseTrailsForm
    {
        private ToolStripMenuItem runOnStartupToolStripMenuItem;
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MouseTrailsForm));
            trailsCountLabel = new Label();
            applyTrails = new Button();
            trailsCountCB = new ComboBox();
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            runOnStartupToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            currentTrailsLabel = new Label();
            currentTrailsUpdateLabel = new Label();
            autoApplyButton = new Button();
            updateRateLabel = new Label();
            updateRateCB = new ComboBox();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // trailsCountLabel
            // 
            trailsCountLabel.AutoSize = true;
            trailsCountLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            trailsCountLabel.Location = new Point(20, 70);
            trailsCountLabel.Name = "trailsCountLabel";
            trailsCountLabel.Size = new Size(96, 25);
            trailsCountLabel.TabIndex = 0;
            trailsCountLabel.Text = "Set Trails:";
            // 
            // applyTrails
            // 
            applyTrails.BackColor = Color.FromArgb(37, 40, 56);
            applyTrails.FlatAppearance.BorderColor = Color.Black;
            applyTrails.FlatAppearance.BorderSize = 0;
            applyTrails.FlatStyle = FlatStyle.Flat;
            applyTrails.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyTrails.Location = new Point(24, 139);
            applyTrails.Name = "applyTrails";
            applyTrails.Size = new Size(92, 40);
            applyTrails.TabIndex = 2;
            applyTrails.Text = "Apply";
            applyTrails.UseVisualStyleBackColor = false;
            applyTrails.Click += applyTrails_Click;
            // 
            // trailsCountCB
            // 
            trailsCountCB.BackColor = Color.FromArgb(53, 57, 74);
            trailsCountCB.DropDownStyle = ComboBoxStyle.DropDownList;
            trailsCountCB.FlatStyle = FlatStyle.Flat;
            trailsCountCB.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            trailsCountCB.ForeColor = Color.White;
            trailsCountCB.FormattingEnabled = true;
            trailsCountCB.Items.AddRange(new object[] { "0", "2", "8", "100" });
            trailsCountCB.Location = new Point(122, 64);
            trailsCountCB.Name = "trailsCountCB";
            trailsCountCB.Size = new Size(90, 33);
            trailsCountCB.TabIndex = 3;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Mouse Trails";
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { runOnStartupToolStripMenuItem, showToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(154, 70);
            // 
            // runOnStartupToolStripMenuItem
            // 
            runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            runOnStartupToolStripMenuItem.Size = new Size(153, 22);
            runOnStartupToolStripMenuItem.Text = "Run on Startup";
            runOnStartupToolStripMenuItem.Click += runOnStartupToolStripMenuItem_Click;
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(153, 22);
            showToolStripMenuItem.Text = "Show";
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(153, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // currentTrailsLabel
            // 
            currentTrailsLabel.AutoSize = true;
            currentTrailsLabel.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            currentTrailsLabel.Location = new Point(18, 23);
            currentTrailsLabel.Name = "currentTrailsLabel";
            currentTrailsLabel.Size = new Size(130, 25);
            currentTrailsLabel.TabIndex = 4;
            currentTrailsLabel.Text = "Current Trails:";
            // 
            // currentTrailsUpdateLabel
            // 
            currentTrailsUpdateLabel.AutoSize = true;
            currentTrailsUpdateLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            currentTrailsUpdateLabel.ForeColor = Color.FromArgb(200, 150, 255);
            currentTrailsUpdateLabel.Location = new Point(142, 23);
            currentTrailsUpdateLabel.Name = "currentTrailsUpdateLabel";
            currentTrailsUpdateLabel.Size = new Size(0, 25);
            currentTrailsUpdateLabel.TabIndex = 5;
            currentTrailsUpdateLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // autoApplyButton
            // 
            autoApplyButton.BackColor = Color.FromArgb(37, 40, 56);
            autoApplyButton.FlatAppearance.BorderSize = 0;
            autoApplyButton.FlatStyle = FlatStyle.Flat;
            autoApplyButton.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            autoApplyButton.Location = new Point(122, 139);
            autoApplyButton.Name = "autoApplyButton";
            autoApplyButton.Size = new Size(90, 40);
            autoApplyButton.TabIndex = 6;
            autoApplyButton.Text = "Auto\r\nUpdate\r\n";
            autoApplyButton.UseVisualStyleBackColor = false;
            autoApplyButton.Click += autoApplyButton_Click;
            // 
            // updateRateLabel
            // 
            updateRateLabel.AutoSize = true;
            updateRateLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            updateRateLabel.Location = new Point(21, 110);
            updateRateLabel.Name = "updateRateLabel";
            updateRateLabel.Size = new Size(106, 17);
            updateRateLabel.TabIndex = 7;
            updateRateLabel.Text = "Set Update Rate:";
            // 
            // updateRateCB
            // 
            updateRateCB.BackColor = Color.FromArgb(53, 57, 74);
            updateRateCB.DropDownStyle = ComboBoxStyle.DropDownList;
            updateRateCB.FlatStyle = FlatStyle.Flat;
            updateRateCB.ForeColor = Color.White;
            updateRateCB.FormattingEnabled = true;
            updateRateCB.Items.AddRange(new object[] { "Every 1s", "Every 2s", "Every 5s", "Every 10s", "Every 30s" });
            updateRateCB.Location = new Point(131, 107);
            updateRateCB.Name = "updateRateCB";
            updateRateCB.Size = new Size(80, 23);
            updateRateCB.TabIndex = 8;
            // 
            // MouseTrailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 11, 14);
            ClientSize = new Size(234, 191);
            Controls.Add(updateRateCB);
            Controls.Add(updateRateLabel);
            Controls.Add(autoApplyButton);
            Controls.Add(currentTrailsUpdateLabel);
            Controls.Add(currentTrailsLabel);
            Controls.Add(trailsCountCB);
            Controls.Add(applyTrails);
            Controls.Add(trailsCountLabel);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MouseTrailsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mouse Trails";
            FormClosing += MouseTrailsForm_FormClosing;
            Load += MouseTrailsForm_Load;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label trailsCountLabel;
        private Button applyTrails;
        private ComboBox trailsCountCB;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem showToolStripMenuItem;
        private Label currentTrailsLabel;
        private Label currentTrailsUpdateLabel;
        private Button autoApplyButton;
        private Label updateRateLabel;
        private ComboBox updateRateCB;
    }
}
