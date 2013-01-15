using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RagnarokHUD
{
    public class ProcessMemoryReader : IDisposable
    {
        private Process process;
        private IntPtr hProcess;

        public IntPtr BaseAddress
        {
            get { return process.MainModule.BaseAddress; }
        }

        public ProcessMemoryReader(Process process)
        {
            OpenProcess(process);
        }

        public ProcessMemoryReader(string processName)
        {
            OpenProcess(GetProcessByName(processName));
        }

        ~ProcessMemoryReader()
        {
            if (process != null)
            {
                process.Close();
                process.Dispose();
            }
            hProcess = IntPtr.Zero;
        }

        private void OpenProcess(Process _process)
        {
            process = _process;

            hProcess = _process.Handle;
        }

        public static Process GetProcessByName(string processName)
        {
            var p = Process.GetProcessesByName(processName);

            if (p.Length == 0)
                throw new Exception(String.Format(CultureInfo.InvariantCulture, "{0} isn't running!", processName));

            return p[0];
        }

        public byte[] Read(IntPtr offset, int length)
        {
            var result = new byte[length];
            WinApi.ReadProcessMemory(hProcess, offset, result, new IntPtr(length), IntPtr.Zero);
            return result;
        }

        public string ReadCString(IntPtr offset, int maxLen)
        {
            return Encoding.UTF8.GetString(Read(offset, maxLen).TakeWhile(ret => ret != 0).ToArray());
        }

        public T Read<T>(IntPtr offset) where T : struct
        {
            byte[] result = new byte[Marshal.SizeOf(typeof(T))];
            WinApi.ReadProcessMemory(hProcess, offset, result, new IntPtr(result.Length), IntPtr.Zero);
            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Pinned);
            var returnObject = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return returnObject;
        }

        public void Dispose()
        {
            process.Close();
            process.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
