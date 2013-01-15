using System;
using System.Globalization;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RagnarokHUD
{
    class Histogram : ContainerControl
    {
        private Pen HistogramBorderPen = new Pen(Color.FromArgb(192, 0, 0, 0));
        private Pen HistogramBorderPen2 = new Pen(Color.FromArgb(255, 0, 0, 0));
        private Brush HistogramBodyBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
        private Pen HistogramValuePen = new Pen(Color.FromArgb(192, 196, 0, 12));
        private Brush HistogramTextBrush = new SolidBrush(Color.FromArgb(255, 128, 128, 0));
        private Font Courier7 = new Font("Courier", 7);

        public enum HistogramType
        {
            Value,
            Average
        }

        private List<Tuple<DateTime, double>> AverageHistorie = new List<Tuple<DateTime, double>>();

        public HistogramType Type { get; set; }

        public double[] HistogramData = new double[4000];
        public int HistogramCount = 0;

        public Histogram(HistogramType type)
        {
            Type = type;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void Reset()
        {
            HistogramData = new double[4000];
            HistogramCount = 0;
            AverageHistorie = new List<Tuple<DateTime, double>>();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.SuspendLayout();
            e.Graphics.DrawRectangle(HistogramBorderPen, 0, 0, this.Width - 80 + 1, this.Height - 1);
            e.Graphics.FillRectangle(HistogramBodyBrush, 1, 1, this.Width - 80, this.Height - 2);
            e.Graphics.DrawString(this.Text, Courier7, Brushes.Black, 1, -1);
            if (HistogramCount > 0)
            {
                double max_v = HistogramData[0];
                for (int i = 1; i < this.Width - 80; i++)
                {
                    double v = HistogramData[i];
                    if (v > max_v) max_v = v;
                }
                double onevh = (this.Height - 2) / max_v;
                for (int i = 0; i < this.Width - 80; i++)
                {
                    double v = HistogramData[i];
                    if (v == 0) continue;
                    int X = 1 + i + (this.Width - 80 - HistogramCount);
                    float H = (float)(v * onevh);
                    e.Graphics.DrawLine(HistogramValuePen, X, this.Height - 1 - H, X, this.Height - 2);
                }
                //if (max_v > 0)
                //{
                var S = (max_v / 1000.0f).ToString("#,0.0K/s max", CultureInfo.InvariantCulture) + "\n" + (HistogramData[HistogramCount - 1] / 1000.0f).ToString("#,0.0K/s last", CultureInfo.InvariantCulture);
                e.Graphics.DrawString(S, Courier7, HistogramTextBrush, this.Width - 80 + 3, 2);
                //}
            }
        }

        public void AddValue(double value)
        {
            switch (Type)
            {
                case HistogramType.Value:
                    if (HistogramCount < this.Width - 80)
                    {
                        HistogramData[HistogramCount] = value;
                        HistogramCount += 1;
                    }
                    else
                    {
                        Array.Copy(HistogramData, 1, HistogramData, 0, HistogramCount - 1);
                        HistogramData[HistogramCount - 1] = value;
                    }
                    break;

                case HistogramType.Average:
                    AverageHistorie.Add(new Tuple<DateTime, double>(DateTime.UtcNow, value));
                    var vals = AverageHistorie.Where(avg => avg.Item1.AddMinutes(1) > DateTime.UtcNow);
                    value = 0.0;
                    foreach (var x in vals)
                    {
                        value += x.Item2;
                    }
                    value = value * 60;

                    AverageHistorie.RemoveAll(x => x.Item1.AddMinutes(1) < DateTime.UtcNow);
                    if (HistogramCount < this.Width - 80)
                    {
                        HistogramData[HistogramCount] = value;
                        HistogramCount += 1;
                    }
                    else
                    {
                        Array.Copy(HistogramData, 1, HistogramData, 0, HistogramCount - 1);
                        HistogramData[HistogramCount - 1] = value;
                    }
                    break;
            }
            this.Invalidate();
        }
    }
}
