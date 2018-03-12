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
    class FreezeAnimals : IInjection
    {
        public bool enabled { get; private set; } = false;
        public IntPtr address { get; private set; } = IntPtr.Zero;
        public IntPtr playerObject { get; set; } = IntPtr.Zero;

        private MemoryClass mem;
        private ProcessModule module;
        private IntPtr hProcess;
        public FreezeAnimals(MemoryClass memParm, ProcessModule moduleParm, IntPtr hProcessParm)
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
            ulong offset = patternscanner.FindPattern("E9 ? ? ? ? 90 90 90 0F 29 45 F0", out timetookms); // The pattern scanner works good and finds correct place
            if (offset > 0)
            {
                if (!mem.WriteByteArray((IntPtr)offset, new byte[] { 0x0F, 0x28, 0x46, 0x60, 0x4C, 0x8D, 0x4D, 0xF0 }))
                    return false;
            }
            else { return false; }
            bool output = Win32.VirtualFreeEx(hProcess, address, 0, AllocationType.Release);
            if (output == true)
                address = IntPtr.Zero;
            return output;
        }
        
        public IntPtr enable(IntPtr addresToUse, ulong playerObject = 0)
        {
            PatternScanner patternscanner = new PatternScanner(hProcess);
            patternscanner.SelectModule(module);
            long timetookms;
            ulong offset = patternscanner.FindPattern("0F 28 46 60 4C 8D 4D F0 0F 29 45 F0", out timetookms); // The pattern scanner works good and finds correct place
            byte[] newBytes = new byte[8];
            byte[] jmpReltiveBytes = mem.toJmpFormat((IntPtr)offset, addresToUse);
            newBytes[0] = 0xE9;
            newBytes[1] = jmpReltiveBytes[0];
            newBytes[2] = jmpReltiveBytes[1];
            newBytes[3] = jmpReltiveBytes[2];
            newBytes[4] = jmpReltiveBytes[3];
            newBytes[5] = 0x90;
            newBytes[6] = 0x90;
            newBytes[7] = 0x90;
            mem.WriteByteArray((IntPtr)offset, newBytes); // The memory class works too

            IntPtr VirtAlloc = Win32.VirtualAllocEx(hProcess, addresToUse, 4096, (uint)AllocationType.Commit | (uint)AllocationType.Reserve, (uint)VirtualMemoryProtection.PAGE_EXECUTE_READWRITE);
            if (VirtAlloc == (IntPtr)0)
                return VirtAlloc;

            //MessageBox.Show(VirtAlloc.ToString() + " | " + Marshal.GetLastWin32Error()); // Show 0 | 487
            jmpReltiveBytes = mem.toJmpFormat(addresToUse + 0x61, module.BaseAddress + 0x13830E3);// I hate magic numbers
            byte[] movReltiveBytes = mem.toJmpFormat(addresToUse + 0xE, (IntPtr)playerObject);// I hate magic numbers
            bool status = mem.WriteByteArray(VirtAlloc, new byte[] { 0x48, 0x39, 0x35, 0x68, 0x00, 0x00, 0x00, 0x74, 0x50, 0x90, 0x90, 0x90, 0x90, 0x51, 0x8B, 0x0D, movReltiveBytes[0], movReltiveBytes[1], movReltiveBytes[2], movReltiveBytes[3], 0x39, 0x8E, 0x84, 0x00, 0x00, 0x00, 0x59, 0x75, 0x19, 0x90, 0x90, 0x90, 0x90, 0x48, 0x89, 0x35, 0x47, 0x00, 0x00, 0x00, 0xEB, 0x2F, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0xC7, 0x46, 0x44, 0x00, 0x00, 0x00, 0x00, 0xC7, 0x46, 0x48, 0x00, 0x00, 0x00, 0x00, 0x45, 0x0F, 0x57, 0xC0, 0x0F, 0x57, 0xFF, 0xEB, 0x0C, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x0F, 0x28, 0x46, 0x60, 0x4C, 0x8D, 0x4D, 0xF0, 0xE9, jmpReltiveBytes[0], jmpReltiveBytes[1], jmpReltiveBytes[2], jmpReltiveBytes[3] });
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
