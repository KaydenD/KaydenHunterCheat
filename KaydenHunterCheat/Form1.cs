using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaydenHunterCheat
{ 
    public partial class Form1 : Form
    {

        Process gameProcess = Process.GetProcessesByName("theHunterCotW_F").FirstOrDefault();
        IntPtr hProcess;
        IntPtr baseaddress;
        MemoryClass mem;
        bool gameOpen = false;
        List<IInjection> listOfInjections = new List<IInjection>();
        applySettings settingsPageApplyStruct = new applySettings() {
            settingsPageReady = false
        };
        StatScanner statscan;
        Timer statRefreshTimer = new Timer();
        public Form1()
        {
            InitializeComponent();
            StatUpdateint.Enabled = false;
            StatUpdateUnit.Enabled = false;
            StatUpdateUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            updateApplyStruct();
            tryLoadingProcess();
        }

        private void tryLoadingProcess()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(tryLoadingProcess));
            }
            else
            {

            gameProcess = Process.GetProcessesByName("theHunterCotW_F").FirstOrDefault();
            if (gameProcess == null)
            {
                MessageBox.Show("Error: The game isn't open");
                TabControls.TabPages[0].Enabled = false;
                TabControls.TabPages[1].Enabled = false;
                foreach (IInjection i in listOfInjections)
                {
                    i.reset();
                }
                foreach (Control ctl in TabControls.TabPages[1].Controls)
                {
                    CheckBox temp = ctl as CheckBox;
                    temp.Checked = false;
                }
                foreach (Control ctl in TabControls.TabPages[0].Controls)
                {
                    if (ctl.GetType() == typeof(TextBox))
                    {
                        TextBox temp = ctl as TextBox;
                        temp.Text = "";
                    }
                }
                gameOpen = false;
                return; 
            }
            baseaddress = gameProcess.MainModule.BaseAddress;
            hProcess = Win32.OpenProcess(2035711U, false, (uint)gameProcess.Id);
            mem = new MemoryClass(hProcess);
            foreach (IInjection i in listOfInjections)
            {
                i.restart(mem, gameProcess.MainModule, hProcess, mem.findAvilMemArea(baseaddress));
            }
            TabControls.TabPages[0].Enabled = true;
            TabControls.TabPages[1].Enabled = true;
            statScanner();
            gameOpen = true;
            statRefreshTimer.Tick += StatRefreshTimer_Tick;
            gameProcess.EnableRaisingEvents = true;
            gameProcess.Exited += GameProcess_Exited;
            return;
            }
        }

        private void StatRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (gameOpen)
            {
                statscan.readStats();
            }
        }

        private void updateApplyStruct()
        {
            int temp;
            if (AutoUpdateStatCheckBox.Checked && settingsPageApplyStruct.settingsPageReady)
            {
                if(!int.TryParse(StatUpdateint.Text, out temp))
                {
                    MessageBox.Show("Error: You must enter a valid number in the Stat Update Interval box");
                    return;
                }
                statRefreshTimer.Interval = (temp * (int)StatUpdateUnit.SelectedValue);
            }

            settingsPageApplyStruct.unitMenuIndex = StatUpdateUnit.SelectedIndex;
            settingsPageApplyStruct.autoUpdateChecked = AutoUpdateStatCheckBox.Checked;
            settingsPageApplyStruct.textbox = StatUpdateint.Text;
            SettingsApply.Enabled = false;
            if (settingsPageApplyStruct.autoUpdateChecked)
            {
                statRefreshTimer.Start();
            }
            else
            {
                statRefreshTimer.Stop();
            }
            
        }

        private void GameProcess_Exited(object sender, EventArgs e)
        {
            tryLoadingProcess();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tryLoadingProcess();
        }


        private void InjectPage_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.kkicon;
            Dictionary<string, int> statupdatedata = new Dictionary<string, int>();
            statupdatedata.Add("MS", 1);
            statupdatedata.Add("S", 1000);
            StatUpdateUnit.DataSource = new BindingSource(statupdatedata, null);
            StatUpdateUnit.DisplayMember = "Key";
            StatUpdateUnit.ValueMember = "Value";
            settingsPageApplyStruct.settingsPageReady = true;

        }



        private void CalmAnimalsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameOpen)
                return;

            if (CalmAnimalsCheckBox.CheckState == CheckState.Checked)
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(CalmAnimals));
                if (index == -1)
                {
                    listOfInjections.Add(new CalmAnimals(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));

            }
            else
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(CalmAnimals));
                if (index != -1)
                    listOfInjections[index].disable();
            }
        }

        private void AutoUpdateStatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoUpdateStatCheckBox.CheckState == CheckState.Checked)
            {
                StatUpdateint.Enabled = true;
                StatUpdateUnit.Enabled = true;
            }
            else
            {
                StatUpdateint.Enabled = false;
                StatUpdateUnit.Enabled = false;
            }
            if(settingsPageApplyStruct.settingsPageReady)
                checkForApply();
        }

        private void StatUpdateint_TextChanged(object sender, EventArgs e)
        {
            if (settingsPageApplyStruct.settingsPageReady)
                checkForApply();
        }

        private void StatUpdateUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (settingsPageApplyStruct.settingsPageReady)
                checkForApply();
        }

        private void checkForApply()
        {
            SettingsApply.Enabled = (AutoUpdateStatCheckBox.Checked != settingsPageApplyStruct.autoUpdateChecked) | (StatUpdateUnit.SelectedIndex != settingsPageApplyStruct.unitMenuIndex) | (StatUpdateint.Text != settingsPageApplyStruct.textbox);
        }
        private void SettingsApply_Click(object sender, EventArgs e)
        {
            updateApplyStruct();
        }

        private void statScanner()
        {

            int index = listOfInjections.FindIndex(x => x.GetType() == typeof(HealthInjector));
            if (index == -1)
            {
                listOfInjections.Add(new HealthInjector(mem, gameProcess.MainModule, hProcess));
                index = listOfInjections.Count - 1;
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));
            }

            List<singleStat> ls = new List<singleStat>();
            ls.Add(new singleStat() { name = MoneyTextBox.Name, textbox = MoneyTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E6A060, (IntPtr)0x298, (IntPtr)0x650 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = XpTextBox.Name, textbox = XpTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E6A060, (IntPtr)0x298, (IntPtr)0x5C4 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = SkillTextBox.Name, textbox = SkillTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E6A060, (IntPtr)0x298, (IntPtr)0x5C8 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = PerkTextBox.Name, textbox = PerkTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E6A060, (IntPtr)0x298, (IntPtr)0x5CC }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = RifleScoreTextBox.Name, textbox = RifleScoreTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E6A060, (IntPtr)0x298, (IntPtr)0x664 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = PistolScoreTextBox.Name, textbox = PistolScoreTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E6A060, (IntPtr)0x298, (IntPtr)0x668 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = ShotgunScoreTextBox.Name, textbox = ShotgunScoreTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E6A060, (IntPtr)0x298, (IntPtr)0x66C }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = BowScoreTextBox.Name, textbox = BowScoreTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E6A060, (IntPtr)0x298, (IntPtr)0x670 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = HealthTextBox.Name, textbox = HealthTextBox, offset = new List<IntPtr>() { (IntPtr)0x26, (IntPtr)0x21C}, sizeInBytes = 2, ishealth = true, healthOffset = listOfInjections[index].address});

            statscan = new StatScanner(ls, mem, gameProcess.MainModule);
            ApplyStatButton.Click += statscan.applyButtonHandler;
        }

        private void ManualUpdateBtn_Click(object sender, EventArgs e)
        {
            statscan.readStats();
        }

        private void CloseCheatBtn_Click(object sender, EventArgs e)
        {
            closeCheat();

        }

        private void closeCheat()
        {
            if (gameOpen)
            {
                statscan.reset();
                foreach (IInjection i in listOfInjections)
                {
                    i.disable();
                    i.reset();
                }

            }
            Application.Exit();
        }

        private void NoVegCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameOpen)
                return;

            if (NoVegCheckBox.CheckState == CheckState.Checked)
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(NoVeg));
                if (index == -1)
                {
                    listOfInjections.Add(new NoVeg(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }
                if (BetterVegCheckBox.CheckState == CheckState.Checked)
                    BetterVegCheckBox.CheckState = CheckState.Unchecked;
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));

            }
            else
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(NoVeg));
                if (index != -1)
                    listOfInjections[index].disable();
            }
        }

        private void UnlimtedAmmoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameOpen)
                return;

            if (UnlimtedAmmoCheckBox.CheckState == CheckState.Checked)
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(InfAmmo));
                if (index == -1)
                {
                    listOfInjections.Add(new InfAmmo(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));

            }
            else
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(InfAmmo));
                if (index != -1)
                    listOfInjections[index].disable();
            }
        }

        private void NoReloadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameOpen)
                return;

            if (NoReloadCheckBox.CheckState == CheckState.Checked)
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(NoReload));
                if (index == -1)
                {
                    listOfInjections.Add(new NoReload(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));

            }
            else
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(NoReload));
                if (index != -1)
                    listOfInjections[index].disable();
            }
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            closeCheat();
        }

        private void BetterVegCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameOpen)
                return;

            if (BetterVegCheckBox.CheckState == CheckState.Checked)
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(BetterVeg));
                if (index == -1)
                {
                    listOfInjections.Add(new BetterVeg(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }

                if (NoVegCheckBox.CheckState == CheckState.Checked)
                    NoVegCheckBox.CheckState = CheckState.Unchecked;
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));

            }
            else
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(BetterVeg));//
                if(index != -1)
                    listOfInjections[index].disable();
            }
        }

        private void NoMapBorderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameOpen)
                return;

            if (NoMapBorderCheckBox.CheckState == CheckState.Checked)
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(NoMapBorders));
                if (index == -1)
                {
                    listOfInjections.Add(new NoMapBorders(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));

            }
            else
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(NoMapBorders));
                if (index != -1)
                    listOfInjections[index].disable();
            }
        }

        private void NoGrassPressureCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameOpen)
                return;

            if (NoGrassPressureCheckBox.CheckState == CheckState.Checked)
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(NoGrassPressure));
                if (index == -1)
                {
                    listOfInjections.Add(new NoGrassPressure(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));

            }
            else
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(NoGrassPressure));
                if (index != -1)
                    listOfInjections[index].disable();
            }
        }

        private void StatsPage_Click(object sender, EventArgs e)
        {

        }

        private void FreezeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!gameOpen)
                return;

            if (FreezeCheckBox.CheckState == CheckState.Checked)
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(PlayerObjInjector));
                if (index == -1)
                {
                    listOfInjections.Add(new PlayerObjInjector(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }
                IntPtr temp = listOfInjections[index].enable(mem.findAvilMemArea(baseaddress));
                index = listOfInjections.FindIndex(x => x.GetType() == typeof(FreezeAnimals));
                if (index == -1)
                {
                    listOfInjections.Add(new FreezeAnimals(mem, gameProcess.MainModule, hProcess));
                    index = listOfInjections.Count - 1;
                }
                listOfInjections[index].enable(mem.findAvilMemArea(baseaddress), (ulong)temp);

            }
            else
            {
                int index = listOfInjections.FindIndex(x => x.GetType() == typeof(PlayerObjInjector));
                if (index != -1)
                    listOfInjections[index].disable();
                index = listOfInjections.FindIndex(x => x.GetType() == typeof(FreezeAnimals));
                if (index != -1)
                    listOfInjections[index].disable();
            }
        }
    }

    struct applySettings
    {
        public string textbox;
        public int unitMenuIndex;
        public bool autoUpdateChecked;
        public bool settingsPageReady;
    }
}
