using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinegamesSandbox
{
    public class ProcessHooks
    {
        static byte[] NtCreateUserProcessOldBytes = new byte[5];
        static byte[] NtOpenProcessOldBytes = new byte[5];
        static byte[] OpenProcessOldBytes = new byte[5];
        static byte[] CreateProcessOldBytes = new byte[4];

        public static void Initialize()
        {
            NtCreateUserProcessOldBytes = Helper.GetBytes_CurrentProcess("NtCreateUserProcess", "ntdll.dll", 5);
            CreateProcessOldBytes = Helper.GetBytes_CurrentProcess("CreateProcessW", "kernelbase.dll", 4);
            NtOpenProcessOldBytes = Helper.GetBytes_CurrentProcess("NtOpenProcess", "ntdll.dll", 5);
            OpenProcessOldBytes = Helper.GetBytes_CurrentProcess("OpenProcess", "kernelbase.dll", 5);
        }

        public static bool PreventProcessCreation(int ProcessID)
        {
            byte[] HookedCode = { 0xC2, 0x2C, 0x00 };
            byte[] HookedCode2 = { 0xC2, 0x28, 0x00 };
            bool NtCreateUserProcess = Helper.HookFunction(ProcessID, "NtCreateUserProcess", "ntdll.dll", HookedCode, 3);
            bool CreateProcessW = Helper.HookFunction(ProcessID, "CreateProcessW", "kernelbase.dll", HookedCode2, 3);
            bool CreateProcessA = Helper.HookFunction(ProcessID, "CreateProcessA", "kernelbase.dll", HookedCode2, 3);
            if (CreateProcessA && CreateProcessW && NtCreateUserProcess)
                return true;
            return false;
        }
        
        public static bool UnPreventProcessCreation(int ProcessID)
        {
            bool NtCreateUserProcessHook = Helper.HookFunction(ProcessID, "NtCreateUserProcess", "ntdll.dll", NtCreateUserProcessOldBytes, 5);
            bool CreateProcessWHook = Helper.HookFunction(ProcessID, "CreateProcessW", "kernelbase.dll", CreateProcessOldBytes, 4);
            bool CreateProcessAHook = Helper.HookFunction(ProcessID, "CreateProcessA", "kernelbase.dll", CreateProcessOldBytes, 4);
            if (NtCreateUserProcessHook && CreateProcessWHook && CreateProcessAHook)
                return true;
            return false;
        }

        public static bool PreventProcessFromGettingProcessHandles(int ProcessID)
        {
            byte[] HookedCode = { 0x89, 0xFF, 0x55, 0x89, 0xE5, 0x5D, 0xC3 };
            byte[] HookedCode2 = { 0xB8, 0x1A, 0x00, 0x00, 0x00, 0xC2, 0x10, 0x00 };
            bool OpenProcessHook = Helper.HookFunction(ProcessID, "OpenProcess", "kernelbase.dll", HookedCode, 7);
            bool NtOpenProcessHook = Helper.HookFunction(ProcessID, "NtOpenProcess", "ntdll.dll", HookedCode2, 8);
            if (OpenProcessHook && NtOpenProcessHook)
                return true;
            return false;
        }

        public static bool UnPreventProcessFromGettingProcessHandles(int ProcessID)
        {
            bool OpenProcessHook = Helper.HookFunction(ProcessID, "OpenProcess", "kernelbase.dll", OpenProcessOldBytes, 10);
            bool NtOpenProcessHook = Helper.HookFunction(ProcessID, "NtOpenProcess", "ntdll.dll", NtOpenProcessOldBytes, 10);
            if (OpenProcessHook && NtOpenProcessHook)
                return true;
            return false;
        }

        public static bool IsHooked_CurrentProcess()
        {
            byte[] HookedCode = { 0xC2, 0x2C, 0x00 };
            byte[] HookedCode2 = { 0xC2, 0x28, 0x00 };
            byte[] CreateProcessWBytes = Helper.GetBytes_CurrentProcess("CreateProcessW", "kernelbase.dll", 1);
            byte[] CreateProcessABytes = Helper.GetBytes_CurrentProcess("CreateProcessA", "kernelbase.dll", 1);
            byte[] NtCreateUserProcess = Helper.GetBytes_CurrentProcess("NtCreateUserProcess", "ntdll.dll", 1);
            foreach (byte NewHookedCode in HookedCode)
            {
                foreach (byte NewHookedCode2 in HookedCode2)
                {
                    if (CreateProcessWBytes[0] == NewHookedCode2 || CreateProcessABytes[0] == NewHookedCode2 || NtCreateUserProcess[0] == NewHookedCode)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsHooked_RemoteProcess(int ProcessID)
        {
            byte[] HookedCode = { 0xC2, 0x2C, 0x00 };
            byte[] HookedCode2 = { 0xC2, 0x28, 0x00 };
            byte[] CreateProcessWBytes = Helper.GetBytes_RemoteProcess(ProcessID ,"CreateProcessW", "kernelbase.dll", 1);
            byte[] CreateProcessABytes = Helper.GetBytes_RemoteProcess(ProcessID ,"CreateProcessA", "kernelbase.dll", 1);
            byte[] NtCreateUserProcess = Helper.GetBytes_RemoteProcess(ProcessID, "NtCreateUserProcess", "ntdll.dll", 1);
            foreach (byte NewHookedCode in HookedCode)
            {
                foreach (byte NewHookedCode2 in HookedCode2)
                {
                    if (CreateProcessWBytes[0] == NewHookedCode2 || CreateProcessABytes[0] == NewHookedCode2 || NtCreateUserProcess[0] == NewHookedCode)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
