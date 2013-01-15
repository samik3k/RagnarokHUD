using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RagnarokHUD
{
    class Memory
    {
        static ProcessMemoryReader Reader;

        static Memory()
        {
            Reader = new ProcessMemoryReader("RagexeRE");
        }

        public static T Read<T>(IntPtr address) where T : struct
        {
            return Reader.Read<T>(new IntPtr(BaseAddress.ToInt64() + address.ToInt64()));
        }

        public static byte[] ReadBytes(IntPtr address, int len)
        {
            return Reader.Read(address, len);
        }

        public static string ReadString(IntPtr address, int maxLen)
        {
            return Reader.ReadCString(new IntPtr(BaseAddress.ToInt64() + address.ToInt64()), maxLen);
        }

        public static IntPtr BaseAddress
        {
            get { return Reader.BaseAddress; }
        }
    }
}
