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
            /* ignore this

            MessageBox.Show(readMoney().ToString()); // work wonders
            */

            // part that finds a pattern and replaces some stuff
            /*
            PatternScanner patternscanner = new PatternScanner(hProcess);
            patternscanner.SelectModule(gameProcess.MainModule);
            long timetookms;
            ulong offset = patternscanner.FindPattern("F3 0F 11 84 8B 68 05 00 00 F3 41 0F 10 00 0F 2F C2", out timetookms); // The pattern scanner works good and finds correct place
            WriteByteArray((IntPtr)offset, new byte[] { 0xE9, 0xE9, 0xDB, 0xBB, 0xFF, 0x90, 0x90, 0x90, 0x90 }); // The memory class works too
            */

            // part that writes to the blank memory from cheat engine
            /*
            IntPtr lpAddress = baseaddress - 0x10000;
            for (; (UInt64)lpAddress > (UInt64)(baseaddress - 0x200000); lpAddress = lpAddress - 0x10000)
            {
                // AllocationType.Commit = 0x1000; VirtualMemoryProtection.PAGE_EXECUTE_READWRITE = 0x40
                VirtAlloc = Win32.VirtualAllocEx(hProcess, lpAddress, 4096, (uint)AllocationType.Commit | (uint)AllocationType.Reserve, (uint)VirtualMemoryProtection.PAGE_EXECUTE_READWRITE);
                if ((UInt64)VirtAlloc != 0)
                    break;
            }
            MessageBox.Show(VirtAlloc.ToString() + " | " + Marshal.GetLastWin32Error()); // Show 0 | 487
            mem.WriteByteArray(lpAddress, new byte[] { 0xC7, 0x84, 0x8B, 0x68, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE9, 0x0B, 0x24, 0x44, 0x00, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            */

            /*
            IntPtr lpAddress = baseaddress - 0x10000; // seems to be the place where cheat engine puts alloc() stuff
            VirtAlloc = VirtualAllocEx(hProcess, lpAddress, 4096, (uint)AllocationType.Commit | (uint)AllocationType.Reserve, (uint)VirtualMemoryProtection.PAGE_EXECUTE_READWRITE);
            MessageBox.Show(VirtAlloc.ToString() + " | " + Marshal.GetLastWin32Error()); // outputs 0 | 487
            */
            tryLoadingProcess();
        }


        #region memoery stuff

        #endregion
        // Every thing in here works as intended
        /*
private void button2_Click(object sender, EventArgs e)
{

// part that finds a pattern and replaces some stuff
PatternScanner patternscanner = new PatternScanner(hProcess);
patternscanner.SelectModule(gameProcess.MainModule);
long timetookms;
ulong offset = patternscanner.FindPattern("F3 0F 11 84 8B 68 05 00 00 F3 41 0F 10 00 0F 2F C2", out timetookms); // The pattern scanner works good and finds correct place
WriteByteArray((IntPtr)offset, new byte[] { 0xE9, 0xE9, 0xDB, 0xBB, 0xFF, 0x90, 0x90, 0x90, 0x90 }); // The memory class works too

// part that writes to the blank memory from cheat engine
IntPtr lpAddress = baseaddress - 0x10000;
WriteByteArray(lpAddress, new byte[] { 0xC7, 0x84, 0x8B, 0x68, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE9, 0x0B, 0x24, 0x44, 0x00, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

PatternScanner patternscanner = new PatternScanner(hProcess);
patternscanner.SelectModule(gameProcess.MainModule);
long timetookms;
ulong offset = patternscanner.FindPattern("E9 E9 DB BB FF 90 90 90 90", out timetookms); // The pattern scanner works good and finds correct place
if(offset > 0)
    mem.WriteByteArray((IntPtr)offset, new byte[] { 0xF3, 0x0F, 0x11, 0x84, 0x8B, 0x68, 0x05, 0x00, 0x00 }); // The memory class works too

// part that writes to the blank memory from cheat engine
Win32.VirtualFreeEx(hProcess, VirtAlloc, 0, AllocationType.Release);


    }
    */
        private void InjectPage_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.kkicon;
            //MessageBox.Show(BitConverter.ToString(toJmpFormat((IntPtr)0x7FF73CBA000B, baseaddress + 0x43241B)));
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
            ls.Add(new singleStat() { name = MoneyTextBox.Name, textbox = MoneyTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xA0 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = XpTextBox.Name, textbox = XpTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0x14 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = SkillTextBox.Name, textbox = SkillTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0x18 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = PerkTextBox.Name, textbox = PerkTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0x1C }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = RifleScoreTextBox.Name, textbox = RifleScoreTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xB4 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = PistolScoreTextBox.Name, textbox = PistolScoreTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xB8 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = ShotgunScoreTextBox.Name, textbox = ShotgunScoreTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xBC }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = BowScoreTextBox.Name, textbox = BowScoreTextBox, offset = new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xC0 }, sizeInBytes = 4 });
            ls.Add(new singleStat() { name = HealthTextBox.Name, textbox = HealthTextBox, offset = new List<IntPtr>() { (IntPtr)0x26, (IntPtr)0x21C}, sizeInBytes = 2, ishealth = true, healthOffset = listOfInjections[index].address});
            /*
             *             Dictionary<TextBox, List<IntPtr>> dict = new Dictionary<TextBox, List<IntPtr>>();
            dict.Add(MoneyTextBox, new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xA0 });
            dict.Add(XpTextBox, new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0x14 });
            dict.Add(SkillTextBox, new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0x18 });
            dict.Add(PerkTextBox, new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0x1C });
            dict.Add(RifleScoreTextBox, new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xB4 });
            dict.Add(PistolScoreTextBox, new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xB8 });
            dict.Add(ShotgunScoreTextBox, new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xBC });
            dict.Add(BowScoreTextBox, new List<IntPtr>() { (IntPtr)0x01E8B7F8, (IntPtr)0x298, (IntPtr)0xC0 });
            */
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
    }

    struct applySettings
    {
        public string textbox;
        public int unitMenuIndex;
        public bool autoUpdateChecked;
        public bool settingsPageReady;
    }
}
