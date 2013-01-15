using System;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RagnarokHUD
{
    class SessionMgr
    {
        // search for an automatic way to find the offset of csession
        // rwc test client
        private const int _session_x86 = 0x005A0838;
        // kRO client
        // private const int _session_x86 = 0x00662988;

        public Session Session;
        
        public SessionMgr()
        {
            try
            {
                Pulse();
            }
            catch (Exception ex)
            {
                // it's ok
            }    
        }

        // update every (x) ms
        public void Pulse()
        {
            try
            {
                // read Session object from memory
                Session = Memory.Read<Session>(new IntPtr(_session_x86));
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }
    }
}
