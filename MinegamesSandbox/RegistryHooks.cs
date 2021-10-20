using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinegamesSandbox
{
    public class RegistryHooks
    {
        public static bool PreventProcessFromEditingOrCreatingRegistryKeys(int ProcessID)
        {
            byte[] HookedCode = { 0xB8, 0x00, 0x00, 0x00, 0x00, 0xC3 };
            bool RegSetValueExWHook = Helper.HookFunction(ProcessID, "RegSetValueExW", "kernelbase.dll", HookedCode, 6);
            bool RegSetValueExAHook = Helper.HookFunction(ProcessID, "RegSetValueExA", "kernelbase.dll", HookedCode, 6);
            bool RegCreateKeyExWHook = Helper.HookFunction(ProcessID, "RegCreateKeyExW", "kernelbase.dll", HookedCode, 6);
            bool RegCreateKeyExAHook = Helper.HookFunction(ProcessID, "RegCreateKeyExA", "kernelbase.dll", HookedCode, 6);
            if (RegSetValueExWHook && RegSetValueExAHook && RegCreateKeyExWHook && RegCreateKeyExAHook)
                return true;
            return false;
        }
    }
}
