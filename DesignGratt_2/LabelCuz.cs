using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignGratt
{
    public class LabelCuz:Label
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath grPath = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            grPath.AddArc(rect.X, rect.Y, rect.Height, rect.Height, 90, 180);
            grPath.AddArc(rect.Width + rect.X - rect.Height, rect.Y, rect.Height, rect.Height, 270, 180);
            Pen pen = Pens.LightGray;
            this.Region = new Region(grPath);
            pevent.Graphics.DrawPath(pen, grPath);

            base.OnPaint(pevent);
        }
    }
}
