using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MinegamesSandbox
{
    class Helper
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr ProcessHandle, IntPtr Address, byte[] Buffer, uint Size, int NumOfBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lib);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr Module, string Function);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr ProcessHandle, IntPtr Address, byte[] Buffer, int Size, int NumOfBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint DesiredAccess, bool InheritHandle, int PID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr Handle);

        public static bool HookFunction(int PID, string FunctionToHook, string LibraryOfFunction, byte[] NewBytes, uint BytesSize)
        {
            IntPtr ProcessHandle = OpenProcess(0x0020 | 0x0008, false, PID);
            IntPtr LibraryModule = GetModuleHandle(LibraryOfFunction);
            IntPtr Function = GetProcAddress(LibraryModule, FunctionToHook);
            bool IsSuccessed = WriteProcessMemory(ProcessHandle, Function, NewBytes, BytesSize, 0);
            CloseHandle(ProcessHandle);
            return IsSuccessed;
        }

        public static IntPtr GetFunction(string Function, string LibraryOfFunction)
        {
            IntPtr LibraryModule = GetModuleHandle(LibraryOfFunction);
            return GetProcAddress(LibraryModule, Function);
        }

        public static byte[] GetBytes_CurrentProcess(string Function, string LibraryOfFunction, int Size)
        {
            byte[] Bytes = new byte[Size];
            Marshal.Copy(GetFunction(Function, LibraryOfFunction), Bytes, 0, Size);
            return Bytes;
        }

        public static byte[] GetBytes_RemoteProcess(int ProcessID, string Function, string LibraryOfFunction, int Size)
        {
            IntPtr ProcessHandle = OpenProcess(0x0020 | 0x0008, false, ProcessID);
            byte[] Bytes = new byte[Size];
            ReadProcessMemory(ProcessHandle, GetFunction(Function, LibraryOfFunction), Bytes, Size, 0);
            CloseHandle(ProcessHandle);
            return Bytes;
        }
    }
}