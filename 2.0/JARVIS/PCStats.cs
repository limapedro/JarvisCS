using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JARVIS
{
    /// <summary>
    /// Classe que obtêm detalhes do sistema.
    /// </summary>
    public static class PCStats
    {
        private static PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static PerformanceCounter ramAvailable = new PerformanceCounter("Memory", "Available MBytes");

        public static double GetCPUUsage()
        {
        	return cpu.NextValue();
        }

        public static float GetFreeMemory()
        {
        	return ramAvailable.NextValue();
        }
        
        public static double GetTotalMemory()
        {
        	return MemoryHelper.GetGlobalMemoryStatusEX() / 1024 / 1024;
        }
    }
    public static class MemoryHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct MEMORYSTATUSEX
        {
            internal uint dwLength;
            internal uint dwMemoryLoad;
            internal ulong ullTotalPhys;
            internal ulong ullAvailPhys;
            internal ulong ullTotalPageFile;
            internal ulong ullAvailPageFile;
            internal ulong ullTotalVirtual;
            internal ulong ullAvailVirtual;
            internal ulong ullAvailExtendedVirtual;
        }
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);
 
        public static double GetGlobalMemoryStatusEX()
        {
            MEMORYSTATUSEX statEX = new MEMORYSTATUSEX();
            statEX.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            GlobalMemoryStatusEx(ref statEX);
 
            return (double)statEX.ullTotalPhys;

        }
    }

}
