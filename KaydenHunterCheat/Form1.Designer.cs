namespace KaydenHunterCheat
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadHunterProcess = new System.Windows.Forms.Button();
            this.ApplyStatButton = new System.Windows.Forms.Button();
            this.TabControls = new System.Windows.Forms.TabControl();
            this.StatsPage = new System.Windows.Forms.TabPage();
            this.ManualUpdateBtn = new System.Windows.Forms.Button();
            this.PistolScoreTextBox = new System.Windows.Forms.TextBox();
            this.BowScoreTextBox = new System.Windows.Forms.TextBox();
            this.ShotgunScoreTextBox = new System.Windows.Forms.TextBox();
            this.RifleScoreTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RifleScoreLabel = new System.Windows.Forms.Label();
            this.MoneyTextBox = new System.Windows.Forms.TextBox();
            this.XpTextBox = new System.Windows.Forms.TextBox();
            this.HealthTextBox = new System.Windows.Forms.TextBox();
            this.SkillTextBox = new System.Windows.Forms.TextBox();
            this.PerkTextBox = new System.Windows.Forms.TextBox();
            this.PerkPointsLabel = new System.Windows.Forms.Label();
            this.SkilPointLabel = new System.Windows.Forms.Label();
            this.HealthLabel = new System.Windows.Forms.Label();
            this.XpLabel = new System.Windows.Forms.Label();
            this.MoneyLabel = new System.Windows.Forms.Label();
            this.InjectPage = new System.Windows.Forms.TabPage();
            this.NoGrassPressureCheckBox = new System.Windows.Forms.CheckBox();
            this.NoMapBorderCheckBox = new System.Windows.Forms.CheckBox();
            this.BetterVegCheckBox = new System.Windows.Forms.CheckBox();
            this.NoVegCheckBox = new System.Windows.Forms.CheckBox();
            this.UnlimtedAmmoCheckBox = new System.Windows.Forms.CheckBox();
            this.NoReloadCheckBox = new System.Windows.Forms.CheckBox();
            this.CalmAnimalsCheckBox = new System.Windows.Forms.CheckBox();
            this.Settings = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.CloseCheatBtn = new System.Windows.Forms.Button();
            this.SettingsApply = new System.Windows.Forms.Button();
            this.StatUpdateUnit = new System.Windows.Forms.ComboBox();
            this.StatUpdateint = new System.Windows.Forms.TextBox();
            this.AutoUpdateStatCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.TabControls.SuspendLayout();
            this.StatsPage.SuspendLayout();
            this.InjectPage.SuspendLayout();
            this.Settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadHunterProcess
            // 
            this.LoadHunterProcess.Location = new System.Drawing.Point(33, 33);
            this.LoadHunterProcess.Name = "LoadHunterProcess";
            this.LoadHunterProcess.Size = new System.Drawing.Size(104, 23);
            this.LoadHunterProcess.TabIndex = 0;
            this.LoadHunterProcess.Text = "Reload Process";
            this.LoadHunterProcess.UseVisualStyleBackColor = true;
            this.LoadHunterProcess.Click += new System.EventHandler(this.button1_Click);
            // 
            // ApplyStatButton
            // 
            this.ApplyStatButton.Location = new System.Drawing.Point(200, 247);
            this.ApplyStatButton.Name = "ApplyStatButton";
            this.ApplyStatButton.Size = new System.Drawing.Size(159, 23);
            this.ApplyStatButton.TabIndex = 1;
            this.ApplyStatButton.Text = "Apply Stats";
            this.ApplyStatButton.UseVisualStyleBackColor = true;
            // 
            // TabControls
            // 
            this.TabControls.Controls.Add(this.StatsPage);
            this.TabControls.Controls.Add(this.InjectPage);
            this.TabControls.Controls.Add(this.Settings);
            this.TabControls.Location = new System.Drawing.Point(12, 12);
            this.TabControls.Name = "TabControls";
            this.TabControls.SelectedIndex = 0;
            this.TabControls.Size = new System.Drawing.Size(581, 302);
            this.TabControls.TabIndex = 2;
            // 
            // StatsPage
            // 
            this.StatsPage.Controls.Add(this.ManualUpdateBtn);
            this.StatsPage.Controls.Add(this.ApplyStatButton);
            this.StatsPage.Controls.Add(this.PistolScoreTextBox);
            this.StatsPage.Controls.Add(this.BowScoreTextBox);
            this.StatsPage.Controls.Add(this.ShotgunScoreTextBox);
            this.StatsPage.Controls.Add(this.RifleScoreTextBox);
            this.StatsPage.Controls.Add(this.label3);
            this.StatsPage.Controls.Add(this.label2);
            this.StatsPage.Controls.Add(this.label1);
            this.StatsPage.Controls.Add(this.RifleScoreLabel);
            this.StatsPage.Controls.Add(this.MoneyTextBox);
            this.StatsPage.Controls.Add(this.XpTextBox);
            this.StatsPage.Controls.Add(this.HealthTextBox);
            this.StatsPage.Controls.Add(this.SkillTextBox);
            this.StatsPage.Controls.Add(this.PerkTextBox);
            this.StatsPage.Controls.Add(this.PerkPointsLabel);
            this.StatsPage.Controls.Add(this.SkilPointLabel);
            this.StatsPage.Controls.Add(this.HealthLabel);
            this.StatsPage.Controls.Add(this.XpLabel);
            this.StatsPage.Controls.Add(this.MoneyLabel);
            this.StatsPage.Location = new System.Drawing.Point(4, 22);
            this.StatsPage.Name = "StatsPage";
            this.StatsPage.Padding = new System.Windows.Forms.Padding(3);
            this.StatsPage.Size = new System.Drawing.Size(573, 276);
            this.StatsPage.TabIndex = 0;
            this.StatsPage.Text = "Stats";
            this.StatsPage.UseVisualStyleBackColor = true;
            this.StatsPage.Click += new System.EventHandler(this.StatsPage_Click);
            // 
            // ManualUpdateBtn
            // 
            this.ManualUpdateBtn.Location = new System.Drawing.Point(473, 247);
            this.ManualUpdateBtn.Name = "ManualUpdateBtn";
            this.ManualUpdateBtn.Size = new System.Drawing.Size(94, 23);
            this.ManualUpdateBtn.TabIndex = 18;
            this.ManualUpdateBtn.Text = "Manual Update";
            this.ManualUpdateBtn.UseVisualStyleBackColor = true;
            this.ManualUpdateBtn.Click += new System.EventHandler(this.ManualUpdateBtn_Click);
            // 
            // PistolScoreTextBox
            // 
            this.PistolScoreTextBox.Location = new System.Drawing.Point(332, 165);
            this.PistolScoreTextBox.Name = "PistolScoreTextBox";
            this.PistolScoreTextBox.Size = new System.Drawing.Size(57, 20);
            this.PistolScoreTextBox.TabIndex = 17;
            // 
            // BowScoreTextBox
            // 
            this.BowScoreTextBox.Location = new System.Drawing.Point(332, 119);
            this.BowScoreTextBox.Name = "BowScoreTextBox";
            this.BowScoreTextBox.Size = new System.Drawing.Size(57, 20);
            this.BowScoreTextBox.TabIndex = 16;
            // 
            // ShotgunScoreTextBox
            // 
            this.ShotgunScoreTextBox.Location = new System.Drawing.Point(332, 78);
            this.ShotgunScoreTextBox.Name = "ShotgunScoreTextBox";
            this.ShotgunScoreTextBox.Size = new System.Drawing.Size(57, 20);
            this.ShotgunScoreTextBox.TabIndex = 15;
            // 
            // RifleScoreTextBox
            // 
            this.RifleScoreTextBox.Location = new System.Drawing.Point(332, 30);
            this.RifleScoreTextBox.Name = "RifleScoreTextBox";
            this.RifleScoreTextBox.Size = new System.Drawing.Size(57, 20);
            this.RifleScoreTextBox.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Pistol Score: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Bow Score: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Shotgun Score: ";
            // 
            // RifleScoreLabel
            // 
            this.RifleScoreLabel.AutoSize = true;
            this.RifleScoreLabel.Location = new System.Drawing.Point(236, 37);
            this.RifleScoreLabel.Name = "RifleScoreLabel";
            this.RifleScoreLabel.Size = new System.Drawing.Size(65, 13);
            this.RifleScoreLabel.TabIndex = 10;
            this.RifleScoreLabel.Text = "Rifle Score: ";
            // 
            // MoneyTextBox
            // 
            this.MoneyTextBox.Location = new System.Drawing.Point(115, 34);
            this.MoneyTextBox.Name = "MoneyTextBox";
            this.MoneyTextBox.Size = new System.Drawing.Size(57, 20);
            this.MoneyTextBox.TabIndex = 9;
            // 
            // XpTextBox
            // 
            this.XpTextBox.Location = new System.Drawing.Point(115, 74);
            this.XpTextBox.Name = "XpTextBox";
            this.XpTextBox.Size = new System.Drawing.Size(57, 20);
            this.XpTextBox.TabIndex = 8;
            // 
            // HealthTextBox
            // 
            this.HealthTextBox.Location = new System.Drawing.Point(115, 123);
            this.HealthTextBox.Name = "HealthTextBox";
            this.HealthTextBox.Size = new System.Drawing.Size(57, 20);
            this.HealthTextBox.TabIndex = 7;
            // 
            // SkillTextBox
            // 
            this.SkillTextBox.Location = new System.Drawing.Point(115, 169);
            this.SkillTextBox.Name = "SkillTextBox";
            this.SkillTextBox.Size = new System.Drawing.Size(57, 20);
            this.SkillTextBox.TabIndex = 6;
            // 
            // PerkTextBox
            // 
            this.PerkTextBox.Location = new System.Drawing.Point(115, 220);
            this.PerkTextBox.Name = "PerkTextBox";
            this.PerkTextBox.Size = new System.Drawing.Size(57, 20);
            this.PerkTextBox.TabIndex = 5;
            // 
            // PerkPointsLabel
            // 
            this.PerkPointsLabel.AutoSize = true;
            this.PerkPointsLabel.Location = new System.Drawing.Point(42, 220);
            this.PerkPointsLabel.Name = "PerkPointsLabel";
            this.PerkPointsLabel.Size = new System.Drawing.Size(67, 13);
            this.PerkPointsLabel.TabIndex = 4;
            this.PerkPointsLabel.Text = "Perk Points: ";
            // 
            // SkilPointLabel
            // 
            this.SkilPointLabel.AutoSize = true;
            this.SkilPointLabel.Location = new System.Drawing.Point(42, 172);
            this.SkilPointLabel.Name = "SkilPointLabel";
            this.SkilPointLabel.Size = new System.Drawing.Size(64, 13);
            this.SkilPointLabel.TabIndex = 3;
            this.SkilPointLabel.Text = "Skill Points: ";
            // 
            // HealthLabel
            // 
            this.HealthLabel.AutoSize = true;
            this.HealthLabel.Location = new System.Drawing.Point(42, 126);
            this.HealthLabel.Name = "HealthLabel";
            this.HealthLabel.Size = new System.Drawing.Size(44, 13);
            this.HealthLabel.TabIndex = 2;
            this.HealthLabel.Text = "Health: ";
            // 
            // XpLabel
            // 
            this.XpLabel.AutoSize = true;
            this.XpLabel.Location = new System.Drawing.Point(42, 81);
            this.XpLabel.Name = "XpLabel";
            this.XpLabel.Size = new System.Drawing.Size(27, 13);
            this.XpLabel.TabIndex = 1;
            this.XpLabel.Text = "XP: ";
            // 
            // MoneyLabel
            // 
            this.MoneyLabel.AutoSize = true;
            this.MoneyLabel.Location = new System.Drawing.Point(42, 37);
            this.MoneyLabel.Name = "MoneyLabel";
            this.MoneyLabel.Size = new System.Drawing.Size(45, 13);
            this.MoneyLabel.TabIndex = 0;
            this.MoneyLabel.Text = "Money: ";
            // 
            // InjectPage
            // 
            this.InjectPage.Controls.Add(this.NoGrassPressureCheckBox);
            this.InjectPage.Controls.Add(this.NoMapBorderCheckBox);
            this.InjectPage.Controls.Add(this.BetterVegCheckBox);
            this.InjectPage.Controls.Add(this.NoVegCheckBox);
            this.InjectPage.Controls.Add(this.UnlimtedAmmoCheckBox);
            this.InjectPage.Controls.Add(this.NoReloadCheckBox);
            this.InjectPage.Controls.Add(this.CalmAnimalsCheckBox);
            this.InjectPage.Location = new System.Drawing.Point(4, 22);
            this.InjectPage.Name = "InjectPage";
            this.InjectPage.Padding = new System.Windows.Forms.Padding(3);
            this.InjectPage.Size = new System.Drawing.Size(573, 276);
            this.InjectPage.TabIndex = 1;
            this.InjectPage.Text = "Injections";
            this.InjectPage.UseVisualStyleBackColor = true;
            this.InjectPage.Click += new System.EventHandler(this.InjectPage_Click);
            // 
            // NoGrassPressureCheckBox
            // 
            this.NoGrassPressureCheckBox.AutoSize = true;
            this.NoGrassPressureCheckBox.Location = new System.Drawing.Point(200, 73);
            this.NoGrassPressureCheckBox.Name = "NoGrassPressureCheckBox";
            this.NoGrassPressureCheckBox.Size = new System.Drawing.Size(114, 17);
            this.NoGrassPressureCheckBox.TabIndex = 8;
            this.NoGrassPressureCheckBox.Text = "No Grass Pressure";
            this.NoGrassPressureCheckBox.UseVisualStyleBackColor = true;
            this.NoGrassPressureCheckBox.CheckedChanged += new System.EventHandler(this.NoGrassPressureCheckBox_CheckedChanged);
            // 
            // NoMapBorderCheckBox
            // 
            this.NoMapBorderCheckBox.AutoSize = true;
            this.NoMapBorderCheckBox.Location = new System.Drawing.Point(200, 31);
            this.NoMapBorderCheckBox.Name = "NoMapBorderCheckBox";
            this.NoMapBorderCheckBox.Size = new System.Drawing.Size(103, 17);
            this.NoMapBorderCheckBox.TabIndex = 7;
            this.NoMapBorderCheckBox.Text = "No Map Borders";
            this.NoMapBorderCheckBox.UseVisualStyleBackColor = true;
            this.NoMapBorderCheckBox.CheckedChanged += new System.EventHandler(this.NoMapBorderCheckBox_CheckedChanged);
            // 
            // BetterVegCheckBox
            // 
            this.BetterVegCheckBox.AutoSize = true;
            this.BetterVegCheckBox.Location = new System.Drawing.Point(39, 202);
            this.BetterVegCheckBox.Name = "BetterVegCheckBox";
            this.BetterVegCheckBox.Size = new System.Drawing.Size(108, 17);
            this.BetterVegCheckBox.TabIndex = 6;
            this.BetterVegCheckBox.Text = "Better Vegetation";
            this.BetterVegCheckBox.UseVisualStyleBackColor = true;
            this.BetterVegCheckBox.CheckedChanged += new System.EventHandler(this.BetterVegCheckBox_CheckedChanged);
            // 
            // NoVegCheckBox
            // 
            this.NoVegCheckBox.AutoSize = true;
            this.NoVegCheckBox.Location = new System.Drawing.Point(39, 155);
            this.NoVegCheckBox.Name = "NoVegCheckBox";
            this.NoVegCheckBox.Size = new System.Drawing.Size(94, 17);
            this.NoVegCheckBox.TabIndex = 5;
            this.NoVegCheckBox.Text = "No Vegetation";
            this.NoVegCheckBox.UseVisualStyleBackColor = true;
            this.NoVegCheckBox.CheckedChanged += new System.EventHandler(this.NoVegCheckBox_CheckedChanged);
            // 
            // UnlimtedAmmoCheckBox
            // 
            this.UnlimtedAmmoCheckBox.AutoSize = true;
            this.UnlimtedAmmoCheckBox.Location = new System.Drawing.Point(39, 116);
            this.UnlimtedAmmoCheckBox.Name = "UnlimtedAmmoCheckBox";
            this.UnlimtedAmmoCheckBox.Size = new System.Drawing.Size(99, 17);
            this.UnlimtedAmmoCheckBox.TabIndex = 4;
            this.UnlimtedAmmoCheckBox.Text = "Unlimted Ammo";
            this.UnlimtedAmmoCheckBox.UseVisualStyleBackColor = true;
            this.UnlimtedAmmoCheckBox.CheckedChanged += new System.EventHandler(this.UnlimtedAmmoCheckBox_CheckedChanged);
            // 
            // NoReloadCheckBox
            // 
            this.NoReloadCheckBox.AutoSize = true;
            this.NoReloadCheckBox.Location = new System.Drawing.Point(39, 73);
            this.NoReloadCheckBox.Name = "NoReloadCheckBox";
            this.NoReloadCheckBox.Size = new System.Drawing.Size(77, 17);
            this.NoReloadCheckBox.TabIndex = 3;
            this.NoReloadCheckBox.Text = "No Reload";
            this.NoReloadCheckBox.UseVisualStyleBackColor = true;
            this.NoReloadCheckBox.CheckedChanged += new System.EventHandler(this.NoReloadCheckBox_CheckedChanged);
            // 
            // CalmAnimalsCheckBox
            // 
            this.CalmAnimalsCheckBox.AutoSize = true;
            this.CalmAnimalsCheckBox.Location = new System.Drawing.Point(39, 31);
            this.CalmAnimalsCheckBox.Name = "CalmAnimalsCheckBox";
            this.CalmAnimalsCheckBox.Size = new System.Drawing.Size(88, 17);
            this.CalmAnimalsCheckBox.TabIndex = 2;
            this.CalmAnimalsCheckBox.Text = "Calm Animals";
            this.CalmAnimalsCheckBox.UseVisualStyleBackColor = true;
            this.CalmAnimalsCheckBox.CheckedChanged += new System.EventHandler(this.CalmAnimalsCheckBox_CheckedChanged);
            // 
            // Settings
            // 
            this.Settings.Controls.Add(this.label4);
            this.Settings.Controls.Add(this.CloseCheatBtn);
            this.Settings.Controls.Add(this.SettingsApply);
            this.Settings.Controls.Add(this.StatUpdateUnit);
            this.Settings.Controls.Add(this.StatUpdateint);
            this.Settings.Controls.Add(this.AutoUpdateStatCheckBox);
            this.Settings.Controls.Add(this.LoadHunterProcess);
            this.Settings.Location = new System.Drawing.Point(4, 22);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(573, 276);
            this.Settings.TabIndex = 2;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "<-------- Can cause crashes";
            // 
            // CloseCheatBtn
            // 
            this.CloseCheatBtn.Location = new System.Drawing.Point(463, 33);
            this.CloseCheatBtn.Name = "CloseCheatBtn";
            this.CloseCheatBtn.Size = new System.Drawing.Size(75, 23);
            this.CloseCheatBtn.TabIndex = 5;
            this.CloseCheatBtn.Text = "Close Cheat";
            this.CloseCheatBtn.UseVisualStyleBackColor = true;
            this.CloseCheatBtn.Click += new System.EventHandler(this.CloseCheatBtn_Click);
            // 
            // SettingsApply
            // 
            this.SettingsApply.Location = new System.Drawing.Point(204, 250);
            this.SettingsApply.Name = "SettingsApply";
            this.SettingsApply.Size = new System.Drawing.Size(170, 23);
            this.SettingsApply.TabIndex = 4;
            this.SettingsApply.Text = "Apply";
            this.SettingsApply.UseVisualStyleBackColor = true;
            this.SettingsApply.Click += new System.EventHandler(this.SettingsApply_Click);
            // 
            // StatUpdateUnit
            // 
            this.StatUpdateUnit.FormattingEnabled = true;
            this.StatUpdateUnit.Location = new System.Drawing.Point(143, 128);
            this.StatUpdateUnit.Name = "StatUpdateUnit";
            this.StatUpdateUnit.Size = new System.Drawing.Size(39, 21);
            this.StatUpdateUnit.TabIndex = 3;
            this.StatUpdateUnit.SelectedIndexChanged += new System.EventHandler(this.StatUpdateUnit_SelectedIndexChanged);
            // 
            // StatUpdateint
            // 
            this.StatUpdateint.Location = new System.Drawing.Point(33, 128);
            this.StatUpdateint.Name = "StatUpdateint";
            this.StatUpdateint.Size = new System.Drawing.Size(104, 20);
            this.StatUpdateint.TabIndex = 2;
            this.StatUpdateint.Text = "Stat Update Interval";
            this.StatUpdateint.TextChanged += new System.EventHandler(this.StatUpdateint_TextChanged);
            // 
            // AutoUpdateStatCheckBox
            // 
            this.AutoUpdateStatCheckBox.AutoSize = true;
            this.AutoUpdateStatCheckBox.Location = new System.Drawing.Point(33, 87);
            this.AutoUpdateStatCheckBox.Name = "AutoUpdateStatCheckBox";
            this.AutoUpdateStatCheckBox.Size = new System.Drawing.Size(113, 17);
            this.AutoUpdateStatCheckBox.TabIndex = 1;
            this.AutoUpdateStatCheckBox.Text = "Auto Update Stats";
            this.AutoUpdateStatCheckBox.UseVisualStyleBackColor = true;
            this.AutoUpdateStatCheckBox.CheckedChanged += new System.EventHandler(this.AutoUpdateStatCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 326);
            this.Controls.Add(this.TabControls);
            this.Name = "Form1";
            this.Text = "The Hunter Stuff";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabControls.ResumeLayout(false);
            this.StatsPage.ResumeLayout(false);
            this.StatsPage.PerformLayout();
            this.InjectPage.ResumeLayout(false);
            this.InjectPage.PerformLayout();
            this.Settings.ResumeLayout(false);
            this.Settings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadHunterProcess;
        private System.Windows.Forms.Button ApplyStatButton;
        private System.Windows.Forms.TabControl TabControls;
        private System.Windows.Forms.TabPage StatsPage;
        private System.Windows.Forms.TextBox PistolScoreTextBox;
        private System.Windows.Forms.TextBox BowScoreTextBox;
        private System.Windows.Forms.TextBox ShotgunScoreTextBox;
        private System.Windows.Forms.TextBox RifleScoreTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RifleScoreLabel;
        private System.Windows.Forms.TextBox MoneyTextBox;
        private System.Windows.Forms.TextBox XpTextBox;
        private System.Windows.Forms.TextBox HealthTextBox;
        private System.Windows.Forms.TextBox SkillTextBox;
        private System.Windows.Forms.TextBox PerkTextBox;
        private System.Windows.Forms.Label PerkPointsLabel;
        private System.Windows.Forms.Label SkilPointLabel;
        private System.Windows.Forms.Label HealthLabel;
        private System.Windows.Forms.Label XpLabel;
        private System.Windows.Forms.Label MoneyLabel;
        private System.Windows.Forms.TabPage InjectPage;
        private System.Windows.Forms.CheckBox CalmAnimalsCheckBox;
        private System.Windows.Forms.TabPage Settings;
        private System.Windows.Forms.CheckBox NoReloadCheckBox;
        private System.Windows.Forms.CheckBox NoMapBorderCheckBox;
        private System.Windows.Forms.CheckBox BetterVegCheckBox;
        private System.Windows.Forms.CheckBox NoVegCheckBox;
        private System.Windows.Forms.CheckBox UnlimtedAmmoCheckBox;
        private System.Windows.Forms.CheckBox NoGrassPressureCheckBox;
        private System.Windows.Forms.ComboBox StatUpdateUnit;
        private System.Windows.Forms.TextBox StatUpdateint;
        private System.Windows.Forms.CheckBox AutoUpdateStatCheckBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button ManualUpdateBtn;
        private System.Windows.Forms.Button SettingsApply;
        private System.Windows.Forms.Button CloseCheatBtn;
        private System.Windows.Forms.Label label4;
    }
}

