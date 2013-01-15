using System;
using System.Globalization;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RagnarokHUD
{
    class Clock : ContainerControl
    {
        public Clock()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        DateTime dateTime;
        float fRadius, fCenterX, fCenterY, fCenterCircleRadius, fHourLength;
        float fMinLength, fSecLength, fHourThickness = 3, fMinThickness = 2, fSecThickness = 1;
        bool bDraw5MinuteTicks = true;
        bool bDraw1MinuteTicks = true;
        float fTicksThickness = 2;

        Color hrColor = Color.Black;
        Color minColor = Color.Black;
        Color secColor = Color.Black;
        Color circleColor = Color.Black;
        Color ticksColor = Color.Black;

        public Color HourHandColor
        {
            get { return this.hrColor; }
            set { this.hrColor = value; }
        }

        public Color MinuteHandColor
        {
            get { return this.minColor; }
            set { this.minColor = value; }
        }

        public Color SecondHandColor
        {
            get { return this.secColor; }
            set
            {
                this.secColor = value;
                this.circleColor = value;
            }
        }

        public Color TicksColor
        {
            get { return this.ticksColor; }
            set { this.ticksColor = value; }
        }

        public bool Draw1MinuteTicks
        {
            get { return this.bDraw1MinuteTicks; }
            set { this.bDraw1MinuteTicks = value; }
        }

        public bool Draw5MinuteTicks
        {

            get { return this.bDraw5MinuteTicks; }
            set { this.bDraw5MinuteTicks = value; }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            this.fRadius = this.Height / 2;
            this.fCenterX = this.ClientSize.Width / 2;
            this.fCenterY = this.ClientSize.Height / 2;
            this.fHourLength = (float)this.Height / 3 / 1.85F;
            this.fMinLength = (float)this.Height / 3 / 1.20F;
            this.fSecLength = (float)this.Height / 3 / 1.15F;
            this.fHourThickness = (float)this.Height / 100;
            this.fMinThickness = (float)this.Height / 150;
            this.fSecThickness = (float)this.Height / 200;
            this.fCenterCircleRadius = this.Height / 50;

            dateTime = DateTime.Now;
            double fRadHr = (dateTime.Hour % 12 + dateTime.Minute / 60F) * 30 * Math.PI / 180;
            double fRadMin = (dateTime.Minute) * 6 * Math.PI / 180;
            double fRadSec = (dateTime.Second) * 6 * Math.PI / 180;

            DrawPolygon(this.fHourThickness, this.fHourLength, hrColor, fRadHr, e);
            DrawPolygon(this.fMinThickness, this.fMinLength, minColor, fRadMin, e);
            DrawLine(this.fSecThickness, this.fSecLength, secColor, fRadSec, e);

            for (int i = 0; i < 60; i++)
            {
                if (this.bDraw5MinuteTicks == true && i % 5 == 0) // Draw 5 minute ticks
                {
                    e.Graphics.DrawLine(new Pen(ticksColor, fTicksThickness),
                        fCenterX + (float)(this.fRadius / 1.50F * System.Math.Sin(i * 6 * Math.PI / 180)),
                        fCenterY - (float)(this.fRadius / 1.50F * System.Math.Cos(i * 6 * Math.PI / 180)),
                        fCenterX + (float)(this.fRadius / 1.65F * System.Math.Sin(i * 6 * Math.PI / 180)),
                        fCenterY - (float)(this.fRadius / 1.65F * System.Math.Cos(i * 6 * Math.PI / 180)));
                }
                else if (this.bDraw1MinuteTicks == true) // draw 1 minute ticks
                {

                    e.Graphics.DrawLine(new Pen(ticksColor, fTicksThickness),
                        fCenterX + (float)(this.fRadius / 1.50F * System.Math.Sin(i * 6 * Math.PI / 180)),
                        fCenterY - (float)(this.fRadius / 1.50F * System.Math.Cos(i * 6 * Math.PI / 180)),
                        fCenterX + (float)(this.fRadius / 1.55F * System.Math.Sin(i * 6 * Math.PI / 180)),
                        fCenterY - (float)(this.fRadius / 1.55F * System.Math.Cos(i * 6 * Math.PI / 180)));
                }
            }
            
            e.Graphics.FillEllipse(new SolidBrush(circleColor), fCenterX - fCenterCircleRadius / 2,
                  fCenterY - fCenterCircleRadius / 2, fCenterCircleRadius, fCenterCircleRadius);

        }

        private void DrawLine(double fThickness, double fLength, Color color, double fRadians,
                              System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(color, (float)fThickness),
                fCenterX - (float)(fLength / 9 * System.Math.Sin(fRadians)),
                fCenterY + (float)(fLength / 9 * System.Math.Cos(fRadians)),
                fCenterX + (float)(fLength * System.Math.Sin(fRadians)),
                fCenterY - (float)(fLength * System.Math.Cos(fRadians)));
        }

        private void DrawPolygon(double fThickness, double fLength, Color color, double fRadians,
                                 System.Windows.Forms.PaintEventArgs e)
        {

            PointF A = new PointF((float)(fCenterX + fThickness * 2 * System.Math.Sin(fRadians + Math.PI / 2)),
                (float)(fCenterY - fThickness * 2 * System.Math.Cos(fRadians + Math.PI / 2)));
            PointF B = new PointF((float)(fCenterX + fThickness * 2 * System.Math.Sin(fRadians - Math.PI / 2)),
                (float)(fCenterY - fThickness * 2 * System.Math.Cos(fRadians - Math.PI / 2)));
            PointF C = new PointF((float)(fCenterX + fLength * System.Math.Sin(fRadians)),
                (float)(fCenterY - fLength * System.Math.Cos(fRadians)));
            PointF D = new PointF((float)(fCenterX - fThickness * 4 * System.Math.Sin(fRadians)),
                (float)(fCenterY + fThickness * 4 * System.Math.Cos(fRadians)));
            PointF[] points = { A, D, B, C };
            e.Graphics.FillPolygon(new SolidBrush(color), points);
        }

        public void Tick()
        {
            this.Invalidate();
        }
    }
}
