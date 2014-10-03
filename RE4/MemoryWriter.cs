using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RE4
{
    class MemoryWriter
    {
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(int hProcess, uint lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        private Process _process;
        private IntPtr _processHandle;
        private uint _baseAddress;

        public MemoryWriter(string processName)
        {
            _process = Process.GetProcessesByName(processName)[0];
            _processHandle = OpenProcess(PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, _process.Id);
            _baseAddress = (uint)_process.MainModule.BaseAddress.ToInt32();
        }

        public void WriteInt16(uint address, Int16 value)
        {
            int bytesWritten = 0;
            byte[] buffer = BitConverter.GetBytes(value);
            uint addr = (_baseAddress + address);
            WriteProcessMemory((int)_processHandle, addr, buffer, buffer.Length, ref bytesWritten);
        }
    }
}
