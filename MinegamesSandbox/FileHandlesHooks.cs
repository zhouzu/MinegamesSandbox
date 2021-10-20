using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MinegamesSandbox
{
    public class FileHandlesHooks
    {
        static byte[] ZwWriteFileOldBytes = new byte[5];
        static byte[] NtReadFileOldBytes = new byte[5];

        public static void Initialize()
        {
            ZwWriteFileOldBytes = Helper.GetBytes_CurrentProcess("ZwWriteFile", "ntdll.dll", 5);
            NtReadFileOldBytes = Helper.GetBytes_CurrentProcess("NtReadFile", "ntdll.dll", 5);
        }

        public static bool PreventWritingFiles(int ProcessID)
        {
            byte[] HookedCode = { 0xC2, 0x18, 0x00 };
            return Helper.HookFunction(ProcessID, "ZwWriteFile", "ntdll.dll", HookedCode, 3);
        }

        public static bool UnPreventWritingFiles(int ProcessID)
        {
            return Helper.HookFunction(ProcessID, "ZwWriteFile", "ntdll.dll", ZwWriteFileOldBytes, 5);
        }

        public static bool PreventReadingFiles(int ProcessID)
        {
            byte[] HookedCode = { 0xC2, 0x18, 0x00 };
            return Helper.HookFunction(ProcessID, "NtReadFile", "ntdll.dll", HookedCode, 3);
        }

        public static bool UnPreventReadingFiles(int ProcessID)
        {
            return Helper.HookFunction(ProcessID, "NtReadFile", "ntdll.dll", NtReadFileOldBytes, 5);
        }

        public static bool IsHooked_CurrentProcess()
        {
            byte[] HookedCode = { 0xC2, 0x18, 0x00 };
            foreach (byte NewHookedCode in HookedCode)
            {
                if (Helper.GetBytes_CurrentProcess("NtReadFile", "ntdll.dll", 3)[0] == NewHookedCode || Helper.GetBytes_CurrentProcess("ZwWriteFile", "ntdll.dll", 3)[0] == NewHookedCode)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsHooked_RemoteProcess(int ProcessID)
        {
            byte[] HookedCode = { 0xC2, 0x18, 0x00 };
            foreach (byte NewHookedCode in HookedCode)
            {
                if (Helper.GetBytes_RemoteProcess(ProcessID, "NtReadFile", "ntdll.dll", 3)[0] == NewHookedCode || Helper.GetBytes_RemoteProcess(ProcessID, "ZwWriteFile", "ntdll.dll", 3)[0] == NewHookedCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}