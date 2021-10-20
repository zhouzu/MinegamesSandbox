using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MinegamesSandbox
{
    public class CLRSandbox
    {
        public static void RunCLRCodeSandboxed(string AssemblyFile, PermissionSet Permissions, bool Enable_DisallowCodeDownload, Evidence Evidence)
        {
            AppDomainSetup Domain = new AppDomainSetup();
            Domain.ApplicationBase = AssemblyFile;
            Domain.DisallowCodeDownload = Enable_DisallowCodeDownload;
            AppDomain Sandbox = AppDomain.CreateDomain("CLR_Code_Sandboxing", Evidence, Domain, Permissions);
            Sandbox.ExecuteAssembly(AssemblyFile);
        }
    }
}