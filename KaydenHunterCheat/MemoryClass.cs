using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaydenHunterCheat
{
    class MemoryClass
    {
        private IntPtr hProcess;

        public MemoryClass(IntPtr process)
        {
            hProcess = process;
        }
    
        public void setProcessHandle(IntPtr handle)
        {
            hProcess = handle;
        }

        public IntPtr findAvilMemArea(IntPtr baseaddress)
        {
            IntPtr lpAddress = baseaddress - 0x10000;
            for (; (UInt64)lpAddress > (UInt64)(baseaddress - 0x200000); lpAddress -= 0x10000)
            {
                // AllocationType.Commit = 0x1000; VirtualMemoryProtection.PAGE_EXECUTE_READWRITE = 0x40
                IntPtr VirtAlloc = Win32.VirtualAllocEx(hProcess, lpAddress, 4096, (uint)AllocationType.Commit | (uint)AllocationType.Reserve, (uint)VirtualMemoryProtection.PAGE_EXECUTE_READWRITE);
                if ((UInt64)VirtAlloc != 0)
                {
                    Win32.VirtualFreeEx(hProcess, VirtAlloc, 0, AllocationType.Release);
                    return VirtAlloc;
                }
            }
            return (IntPtr)0;
        }

        public IntPtr addresFromOffsetList(List<IntPtr> list, IntPtr baseaddress)
        {
            if (list.Count < 1)
                throw new Exception("List Too Short");

            IntPtr CurOffset = (IntPtr)ReadUInt64(IntPtr.Add(baseaddress, (int)list[0]));

            for(int i = 1; i < list.Count - 1; i++)
            {
                CurOffset = (IntPtr)ReadUInt64(IntPtr.Add(CurOffset, (int)list[i]));
            }
            if (list.Count >= 2)
            {
                return IntPtr.Add(CurOffset, (int)list.Last());
            }
            else
            {
                return CurOffset;
            }
        }

        public byte[] toJmpFormat(IntPtr instrutAddres, IntPtr target)
        {
            byte[] bytearray = BitConverter.GetBytes((ulong)target - (ulong)(instrutAddres + 0x5));

            return new byte[] { bytearray[0], bytearray[1], bytearray[2], bytearray[3] };
        }

        public IntPtr fromJmpFormat(IntPtr instrutAddres, byte[] jmpStuff)
        {
            int jew = BitConverter.ToInt32(jmpStuff, 0);
            IntPtr instuctloc = instrutAddres + 0x5;
            return instuctloc + jew;
        }


       public byte[] toMovFormat(IntPtr instrutAddres, IntPtr target)
        {
            byte[] bytearray = BitConverter.GetBytes((ulong)target - (ulong)(instrutAddres + 0x6));

            return new byte[] { bytearray[0], bytearray[1], bytearray[2], bytearray[3] };
        }

        public IntPtr fromMovFormat(IntPtr instrutAddres, byte[] jmpStuff)
        {
            int jew = BitConverter.ToInt32(jmpStuff, 0);
            IntPtr instuctloc = instrutAddres + 0x6;
            return instuctloc + jew;
        }

        public byte[] ReadByteArray(IntPtr pOffset, uint pSize)
        {
            if (hProcess == IntPtr.Zero)
                throw new Exception("process is fucked");
            try
            {
                uint lpflOldProtect;
                Win32.VirtualProtectEx(hProcess, pOffset, (UIntPtr)pSize, (uint)VirtualMemoryProtection.PAGE_READWRITE, out lpflOldProtect);
                byte[] lpBuffer = new byte[pSize];
                Win32.ReadProcessMemory(hProcess, pOffset, lpBuffer, pSize, 0U);
                Win32.VirtualProtectEx(hProcess, pOffset, (UIntPtr)pSize, lpflOldProtect, out lpflOldProtect);
                return lpBuffer;
            }
            catch
            {
                throw new Exception("it broke");
            }
        }

        public int ReadInt32(IntPtr pOffset)
        {
            if (hProcess == IntPtr.Zero)
                throw new Exception("process is fucked");
            try
            {
                return BitConverter.ToInt32(this.ReadByteArray(pOffset, 4U), 0);
            }
            catch
            {
                return 0;
            }
        }

        public Int16 ReadInt16(IntPtr pOffset)
        {
            if (hProcess == IntPtr.Zero)
                throw new Exception("process is fucked");
            try
            {
                return BitConverter.ToInt16(this.ReadByteArray(pOffset, 2U), 0);
            }
            catch
            {
                return 0;
            }
        }

        public ulong ReadUInt64(IntPtr pOffset)
        {
            if (hProcess == IntPtr.Zero)
                throw new Exception("process is fucked");
            try
            {
                return BitConverter.ToUInt64(this.ReadByteArray(pOffset, 8U), 0);
            }
            catch
            {
                return 0;
            }
        }

        public bool WriteByteArray(IntPtr pOffset, byte[] pBytes)
        {
            if (hProcess == IntPtr.Zero)
                throw new Exception("process is fucked");
            try
            {
                uint lpflOldProtect;
                Win32.VirtualProtectEx(hProcess, pOffset, (UIntPtr)((ulong)pBytes.Length), 4U, out lpflOldProtect);
                bool flag = Win32.WriteProcessMemory(hProcess, pOffset, pBytes, (uint)pBytes.Length, 0U);
                Win32.VirtualProtectEx(hProcess, pOffset, (UIntPtr)((ulong)pBytes.Length), lpflOldProtect, out lpflOldProtect);
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public bool WriteInt32(IntPtr pOffset, int pData)
        {
            if (hProcess == IntPtr.Zero)
                throw new Exception("process is fucked");
            try
            {
                return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
            }
            catch
            {
                return false;
            }
        }

        public bool WriteInt16(IntPtr pOffset, short pData)
        {
            if (hProcess == IntPtr.Zero)
                throw new Exception("process is fucked");
            try
            {
                return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
            }
            catch
            {
                return false;
            }
        }
    }
}
