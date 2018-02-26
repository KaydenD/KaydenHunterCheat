using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KaydenHunterCheat
{
    class NoMapBorders : IInjection
    {
        public bool enabled { get; private set; } = false;
        public IntPtr address { get; private set; } = IntPtr.Zero;


        private MemoryClass mem;
        private ProcessModule module;
        private IntPtr hProcess;
        public NoMapBorders(MemoryClass memParm, ProcessModule moduleParm, IntPtr hProcessParm)
        {
            mem = memParm;
            module = moduleParm;
            hProcess = hProcessParm;
        }


        public bool disable()
        {
            PatternScanner patternscanner = new PatternScanner(hProcess);
            patternscanner.SelectModule(module);
            long timetookms;
            ulong offset = patternscanner.FindPattern("5B C3 90 90 90 90 90 90 90 74 0F", out timetookms) + 0x2; // The pattern scanner works good and finds correct place
            if (offset > 0 && offset == (ulong)address)
            {
                if (!mem.WriteByteArray((IntPtr)offset, new byte[] { 0x80, 0xBB, 0xE2, 0x01, 0x00, 0x00, 0x00 }))
                    return false;
            }
            else { return false; }
            address = IntPtr.Zero;
            return true;
        }

        public IntPtr enable(IntPtr addresToUse)
        {
            PatternScanner patternscanner = new PatternScanner(hProcess);
            patternscanner.SelectModule(module);
            long timetookms;
            ulong offset = patternscanner.FindPattern("5B C3 80 BB E2 01 00 00 00 74 0F", out timetookms) + 0x2; // The pattern scanner works good and finds correct place
            mem.WriteByteArray((IntPtr)offset, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }); // The memory class works too

            address = (IntPtr)offset;

            return address;
        }

        public void restart(MemoryClass memParm, ProcessModule moduleParm, IntPtr hProcessParm, IntPtr addresToUse)
        {
            mem = memParm;
            module = moduleParm;
            hProcess = hProcessParm;
        }

        public void reset()
        {
            enabled = false;
            address = IntPtr.Zero;
        }
    }
}
