using System.Reflection;
using System.Runtime.Loader;

namespace HtmlToPdf.Helpers
{
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllPath)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null; // ما نیازی به بارگذاری اسمبلی .NET نداریم، فقط native DLL ها رو بارگذاری می‌کنیم
        }
    }
}
