using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RagnarokHUD
{
    public partial class fHUD : Form
    {
        public Process Ragexe;
        public Dictionary<string, ContainerControl> Layers;

        private Thread _refreshThread;
        private SessionMgr _sessionMgr;

        public fHUD()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WinApi.SetWindowPos(this.Handle, WinApi.HWND_TOPMOST, 0, 0, 0, 0, WinApi.TOPMOST_FLAGS);
            _refreshThread = new Thread(RefreshThread);

            _sessionMgr = new SessionMgr();

            Layers = new Dictionary<string, ContainerControl>();

            AddHistogram("EXP_HISTOGRAM", "exp", 250, 30, 900, 40, Histogram.HistogramType.Average);
            AddHistogram("EXP_HISTOGRAM2", "exp2", 250, 30, 900, 80, Histogram.HistogramType.Value);
            AddHistogram("JEXP_HISTOGRAM", "jexp", 250, 30, 900, 120, Histogram.HistogramType.Average);

            Layers.Add("CLOCK", new Clock());
            var c = Layers["CLOCK"];
            c.Width = 250;
            c.Height = 250;
            c.Left = 900;
            c.Top = 90;
            this.Controls.Add(Layers["CLOCK"]);

            var rect = new RECT();
            var Ragexes = Process.GetProcessesByName("RagexeRE");
            if (Ragexes.Count() > 0)
            {
                var get = WinApi.GetWindowRect(Ragexes[0].MainWindowHandle, out rect);
                this.Location = new Point(rect.left, rect.top);
                this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            }
            _refreshThread.Start();
        }

        private void AddHistogram(string key, string text, int width, int height, int left, int top, Histogram.HistogramType type)
        {
            Layers.Add(key, new Histogram(type) { Text = text });
            var c = Layers[key];
            c.Width = width;
            c.Height = height;
            c.Left = left;
            c.Top = top;
            this.Controls.Add(Layers[key]);
        }

        delegate void PaintCallback(RECT rect);
        private new void Paint(RECT rect)
        {
            if (this.InvokeRequired)
            {
                PaintCallback d = new PaintCallback(Paint);
                this.Invoke(d, new object[] { rect });
            }
            else
            {
                this.Location = new Point(rect.left, rect.top);
                this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            }
        }

        private DateTime _lastTick;
        private int _exp = -1;
        private int _jexp = -1;
        private string _lastName = string.Empty;

        private void RefreshThread()
        {
            while (true)
            {
                var Ragexes = Process.GetProcessesByName("RagexeRE");
                if (Ragexes.Count() > 0)
                {
                    var rect = new RECT();
                    var get = WinApi.GetWindowRect(Ragexes[0].MainWindowHandle, out rect);
                    this.Paint(rect);

                    IntPtr hwnd = WinApi.GetForegroundWindow();
                    if (hwnd != Ragexes[0].MainWindowHandle)
                    {
                        this.Hide();
                        Pulse();
                    }
                    else
                    {
                        this.Show();
                    }
                }
                else
                {
                    this.Hide();
                }
                Thread.Sleep(10);
            }
        }

        private void Pulse()
        {
            var name = WinApi.CharToString(_sessionMgr.Session.m_cName);
            if (name != _lastName)
            {
                _exp = -1;
                _jexp = -1;
                _lastName = WinApi.CharToString(_sessionMgr.Session.m_cName);
                foreach (var h in Layers.Where(l => l.Value.GetType() == typeof(Histogram)))
                {
                    var x = (Histogram)(h.Value);
                    x.Reset();
                }
            }
            if (_lastTick.AddSeconds(1) < DateTime.UtcNow)
            {
                var x = Newtonsoft.Json.JsonConvert.SerializeObject(_sessionMgr.Session);
                _lastTick = DateTime.UtcNow;

                ((Clock)Layers["CLOCK"]).Tick();
                if (_sessionMgr != null)
                    _sessionMgr.Pulse();

                if (_exp != -1 && _jexp != -1 && _lastName != string.Empty)
                {
                    var val = _sessionMgr.Session.m_exp - _exp;
                    if (val < 0)
                    {
                        _exp = 0;
                        val = _sessionMgr.Session.m_exp - _exp;
                    }
                    ((Histogram)Layers["EXP_HISTOGRAM"]).AddValue(val);
                    ((Histogram)Layers["EXP_HISTOGRAM2"]).AddValue(val);

                    val = _sessionMgr.Session.m_jobexp - _jexp;
                    if (val < 0)
                    {
                        _jexp = 0;
                        val = _sessionMgr.Session.m_jobexp - _jexp;
                    }
                    ((Histogram)Layers["JEXP_HISTOGRAM"]).AddValue(val);
                }
                _exp = _sessionMgr.Session.m_exp;
                _jexp = _sessionMgr.Session.m_jobexp;
            }
        }

        Random r = new Random();
        private int RandomNumber(int min, int max)
        {
            return r.Next(min, max);
        }
    }
}
