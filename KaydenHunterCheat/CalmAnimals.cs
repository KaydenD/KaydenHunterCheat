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
    interface IInjection
    {
        bool enabled { get; }

        bool disable();

        void reset();
        IntPtr address { get; }

        IntPtr enable(IntPtr addresToUse);

        void restart(MemoryClass memParm, ProcessModule moduleParm, IntPtr hProcessParm, IntPtr addresToUse);
    }

    class CalmAnimals : IInjection
    {
        public bool enabled { get; private set; } = false;
        public IntPtr address { get; private set; } = IntPtr.Zero;


        private MemoryClass mem;
        private ProcessModule module;
        private IntPtr hProcess;
        public CalmAnimals(MemoryClass memParm, ProcessModule moduleParm, IntPtr hProcessParm)
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
            ulong offset = patternscanner.FindPattern("E9 ? ? ? ? 90 90 90 90 F3 0F 10 84 8B 54 05", out timetookms); // The pattern scanner works good and finds correct place
            if (offset > 0)
            {
                if (!mem.WriteByteArray((IntPtr)offset, new byte[] { 0xF3, 0x0F, 0x11, 0x84, 0x8B, 0x68, 0x05, 0x00, 0x00 }))
                    return false;
            }
            else { return false; }
            bool output = Win32.VirtualFreeEx(hProcess, address, 0, AllocationType.Release);
            if (output == true)
                address = IntPtr.Zero;
            return output;
        }

        public IntPtr enable(IntPtr addresToUse)
        {
            PatternScanner patternscanner = new PatternScanner(hProcess);
            patternscanner.SelectModule(module);
            long timetookms;
            ulong offset = patternscanner.FindPattern("F3 0F 11 84 8B 68 05 00 00 F3 0F 10 84 8B 54 05", out timetookms); // The pattern scanner works good and finds correct place
            byte[] newBytes = new byte[9];
            byte[] jmpReltiveBytes = mem.toJmpFormat((IntPtr)offset, addresToUse);
            newBytes[0] = 0xE9;
            newBytes[1] = jmpReltiveBytes[0];
            newBytes[2] = jmpReltiveBytes[1];
            newBytes[3] = jmpReltiveBytes[2];
            newBytes[4] = jmpReltiveBytes[3];
            newBytes[5] = 0x90;
            newBytes[6] = 0x90;
            newBytes[7] = 0x90;
            newBytes[8] = 0x90;
            mem.WriteByteArray((IntPtr)offset, newBytes); // The memory class works too

            IntPtr VirtAlloc = Win32.VirtualAllocEx(hProcess, addresToUse, 4096, (uint)AllocationType.Commit | (uint)AllocationType.Reserve, (uint)VirtualMemoryProtection.PAGE_EXECUTE_READWRITE);
            if (VirtAlloc == (IntPtr)0)
                return VirtAlloc;

            //MessageBox.Show(VirtAlloc.ToString() + " | " + Marshal.GetLastWin32Error()); // Show 0 | 487
            jmpReltiveBytes = mem.toJmpFormat(addresToUse + 0xB, module.BaseAddress + 0x441DE5);
            bool status = mem.WriteByteArray(VirtAlloc, new byte[] { 0xC7, 0x84, 0x8B, 0x68, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE9, jmpReltiveBytes[0], jmpReltiveBytes[1], jmpReltiveBytes[2], jmpReltiveBytes[3], 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            if(status != true)
            {
                Win32.VirtualFreeEx(hProcess, VirtAlloc, 0, AllocationType.Release);
                return (IntPtr)0;
            }

            address = VirtAlloc;

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
