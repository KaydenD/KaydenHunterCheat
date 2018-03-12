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
    class NoGrassPressure : IInjection
    {
        public bool enabled { get; private set; } = false;
        public IntPtr address { get; private set; } = IntPtr.Zero;


        private MemoryClass mem;
        private ProcessModule module;
        private IntPtr hProcess;
        public NoGrassPressure(MemoryClass memParm, ProcessModule moduleParm, IntPtr hProcessParm)
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
            ulong offset = patternscanner.FindPattern("48 63 05 F5 78 C5 01 E9 ? ? ? ? 90 C3 CC", out timetookms) + 0x7; // The pattern scanner works good and finds correct place
            if (offset > 0)
            {
                if (!mem.WriteByteArray((IntPtr)offset, new byte[] { 0xF3, 0x0F, 0x11, 0x4C, 0x81, 0x30 }))
                    return false;
            }
            else { return false; }
            bool output = Win32.VirtualFreeEx(hProcess, address, 0, AllocationType.Release);
            if (output == true)
                address = IntPtr.Zero;
            enabled = false;
            return output;
        }

        public IntPtr enable(IntPtr addresToUse, ulong playerObject = 0)
        {
            PatternScanner patternscanner = new PatternScanner(hProcess);
            patternscanner.SelectModule(module);
            long timetookms;
            ulong offset = patternscanner.FindPattern("F3 0F 11 4C 81 30 C3 CC", out timetookms); // The pattern scanner works good and finds correct place
            byte[] newBytes = new byte[6];
            byte[] jmpReltiveBytes = mem.toJmpFormat((IntPtr)offset, addresToUse);
            newBytes[0] = 0xE9;
            newBytes[1] = jmpReltiveBytes[0];
            newBytes[2] = jmpReltiveBytes[1];
            newBytes[3] = jmpReltiveBytes[2];
            newBytes[4] = jmpReltiveBytes[3];
            newBytes[5] = 0x90;
            mem.WriteByteArray((IntPtr)offset, newBytes); // The memory class works too

            IntPtr VirtAlloc = Win32.VirtualAllocEx(hProcess, addresToUse, 4096, (uint)AllocationType.Commit | (uint)AllocationType.Reserve, (uint)VirtualMemoryProtection.PAGE_EXECUTE_READWRITE);
            if (VirtAlloc == (IntPtr)0)
                return VirtAlloc;

            //MessageBox.Show(VirtAlloc.ToString() + " | " + Marshal.GetLastWin32Error()); // Show 0 | 487
            jmpReltiveBytes = mem.toJmpFormat(addresToUse + 0x8, module.BaseAddress + 0x15200D);
            bool status = mem.WriteByteArray(VirtAlloc, new byte[] { 0xC7, 0x44, 0x81, 0x30, 0x00, 0x00, 0x00, 0x00, 0xE9, jmpReltiveBytes[0], jmpReltiveBytes[1], jmpReltiveBytes[2], jmpReltiveBytes[3], 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            if(status != true)
            {
                Win32.VirtualFreeEx(hProcess, VirtAlloc, 0, AllocationType.Release);
                return (IntPtr)0;
            }

            address = VirtAlloc;
            enabled = true;
            return VirtAlloc;
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
