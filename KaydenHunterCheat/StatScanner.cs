using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace KaydenHunterCheat
{
    class StatScanner
    {
        MemoryClass mem;
        ProcessModule module;
        statPage statpage;

        bool ready = false;

        public StatScanner(List<singleStat> list, MemoryClass memPram, ProcessModule proccesMod)
        {
            mem = memPram;
            module = proccesMod;
            statpage = new statPage();
            statpage.stats = list;
            /*
            foreach (KeyValuePair<TextBox, List<IntPtr>> x in dict)
            {
                singleStat temp = new singleStat()
                {
                    name = x.Key.Name,
                    textbox = x.Key,
                    offset = x.Value
                };
                statpage.stats.Add(temp);
            }
            */
            ready = true;
            //readStats();
        }

        public void applyButtonHandler(object sender, EventArgs e)
        {
            //IntPtr offset1 = (IntPtr)mem.ReadUInt64(IntPtr.Add(module.BaseAddress, 0x01E8B7F8));
            //IntPtr offset2 = (IntPtr)mem.ReadUInt64(IntPtr.Add(offset1, 0x298));
            //return mem.ReadInt32(offset2 + 0xa0);
            for (int i = 0; i < statpage.stats.Count; i++)
            {
                int temp;
                if (!int.TryParse(statpage.stats[i].textbox.Text, out temp)) {
                    MessageBox.Show("Error: " + statpage.stats[i].name + " isn't a vaild number");
                    break;
                }

                IntPtr offset = mem.addresFromOffsetList(statpage.stats[i].offset, module.BaseAddress);
                bool thing;
                if (statpage.stats[i].ishealth)
                {
                    offset = mem.addresFromOffsetList(statpage.stats[i].offset, statpage.stats[i].healthOffset);
                    thing = mem.WriteInt16(offset, (short)temp);
                }

                thing = mem.WriteInt32(offset, temp);
                //int temp = mem.ReadInt32(IntPtr.Add(offset2, (int)statpage.stats[i].offset));
            }
        }

        public void readStats()
        {
            if (ready)
            {
                //IntPtr offset1 = (IntPtr)mem.ReadUInt64(IntPtr.Add(module.BaseAddress, 0x01E8B7F8));
                //IntPtr offset2 = (IntPtr)mem.ReadUInt64(IntPtr.Add(offset1, 0x298));
                //return mem.ReadInt32(offset2 + 0xa0);
                for (int i = 0; i < statpage.stats.Count; i++)
                {
                    IntPtr offset = mem.addresFromOffsetList(statpage.stats[i].offset, module.BaseAddress);
                    if(statpage.stats[i].ishealth)
                        offset = mem.addresFromOffsetList(statpage.stats[i].offset, statpage.stats[i].healthOffset);
                    int value = 0;
                    if (statpage.stats[i].sizeInBytes == 4)
                    {
                        value = mem.ReadInt32(offset);
                    }
                    else if(statpage.stats[i].sizeInBytes == 2)
                    {
                        value = mem.ReadInt16(offset);
                    }
                    if (value != statpage.stats[i].value)
                    {
                        statpage.stats[i].textbox.Text = value.ToString();
                        statpage.stats[i].value = value;
                    }
                }
            }
        }

        public void update(MemoryClass memPram, ProcessModule proccesMod)
        {
            mem = memPram;
            module = proccesMod;
            ready = true;
        }

        public void reset()
        {
            ready = false;
            mem = null;
            module = null;
        }
    }

    class singleStat
    {
        public string name;
        public TextBox textbox;
        public List<IntPtr> offset;
        public int sizeInBytes;
        public int value;
        public bool ishealth = false;
        public IntPtr healthOffset;
    }

    class statPage
    {
        public List<singleStat> stats;
    }
}
