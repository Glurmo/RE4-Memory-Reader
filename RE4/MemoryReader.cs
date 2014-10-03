using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace RE4
{
    class MemoryReader
    {
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, uint lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        private Process _process;
        private IntPtr _processHandle;
        private uint _baseAddress;

        public MemoryReader(string processName)
        {
            _process = Process.GetProcessesByName(processName)[0];
            _processHandle = OpenProcess(PROCESS_WM_READ, false, _process.Id);
            _baseAddress = (uint)_process.MainModule.BaseAddress.ToInt32();
        }

        private byte[] _readBytes(uint address, int length)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[length];
            uint addr = (_baseAddress + address);
            ReadProcessMemory((int)_processHandle, addr, buffer, buffer.Length, ref bytesRead);
            return buffer;
        }

        public Byte ReadByte(uint address)
        {
            return _readBytes(address, 1).First();
        }

        public Int16 ReadInt16(uint address)
        {
            return BitConverter.ToInt16(_readBytes(address, 2), 0);
        }

        public Int32 ReadInt32(uint address)
        {
            return BitConverter.ToInt32(_readBytes(address, 4), 0);
        }

        public bool IsProcessOpen()
        {
            return !_process.HasExited;
        }

    }
}
