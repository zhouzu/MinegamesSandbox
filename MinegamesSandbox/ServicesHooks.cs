using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinegamesSandbox
{
    public class ServicesHooks
    {
        static byte[] CreateServiceA = new byte[5];
        static byte[] CreateServiceW = new byte[5];

        public static void Initialize()
        {
            CreateServiceA = Helper.GetBytes_CurrentProcess("CreateServiceA", "sechost.dll", 1);
            CreateServiceW = Helper.GetBytes_CurrentProcess("CreateServiceW", "sechost.dll", 1);
        }

        public static bool PreventCreatingServices(int ProcessID)
        {
            byte[] HookedCode = { 0xC3 };
            bool CreateServiceAHook = Helper.HookFunction(ProcessID, "CreateServiceA", "sechost.dll", HookedCode, 1);
            bool CreateServiceWHook = Helper.HookFunction(ProcessID, "CreateServiceW", "sechost.dll", HookedCode, 1);
            if (CreateServiceAHook && CreateServiceWHook)
                return true;
            return false;
        }

        public static bool UnPreventCreatingServices(int ProcessID)
        {
            bool CreateServiceAUnHook = Helper.HookFunction(ProcessID, "CreateServiceA", "sechost.dll", CreateServiceA, 5);
            bool CreateServiceWUnHook = Helper.HookFunction(ProcessID, "CreateServiceW", "sechost.dll", CreateServiceW, 5);
            if (CreateServiceAUnHook && CreateServiceWUnHook)
                return true;
            return false;
        }

        public static bool IsHooked_CurrentProcess()
        {
            if (Helper.GetBytes_CurrentProcess("CreateServiceA", "sechost.dll", 1)[0] == 0xC3 || Helper.GetBytes_CurrentProcess("CreateServiceA", "sechost.dll", 1)[0] == 0xC3)
            {
                return true;
            }
            return false;
        }

        public static bool IsHooked_RemoteProcess(int ProcessID)
        {
            if (Helper.GetBytes_RemoteProcess(ProcessID, "CreateServiceW", "sechost.dll", 3)[0] == 0xC3 || Helper.GetBytes_RemoteProcess(ProcessID, "CreateServiceW", "sechost.dll", 3)[0] == 0xC3)
            {
                return true;
            }
            return false;
        }
    }
}