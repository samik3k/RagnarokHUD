using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RagnarokHUD
{
    public class Layer : ContainerControl
    {
        public override void Refresh()
        {
            base.Refresh();
            this.Width = Convert.ToInt32(0.29 * this.Parent.Size.Height);
            this.Height = Convert.ToInt32(0.29 * this.Parent.Size.Height);
            this.Left = Convert.ToInt32(0.905 * this.Parent.Size.Width - this.Width / 2);
            this.Top = Convert.ToInt32(0.19 * this.Parent.Size.Height - this.Height / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            Pen RedPen = new Pen(Color.Red, 1);
            dc.DrawRectangle(RedPen, this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            dc.DrawRectangle(RedPen, 0, 0, 50, 50);
        }
    }
}
