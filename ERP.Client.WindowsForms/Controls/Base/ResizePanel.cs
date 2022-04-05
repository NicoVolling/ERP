using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Controls.Base
{
    public class ResizePanel : Panel
    {
        [Category("Steuerung")]
        public Control Resizeable { get; set; }

        private bool mouseDown = false;

        private Point mousePosition;

        private Size startSize = new Size(0, 0);

        private Size endSize = new Size(0, 0);

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            float X = 0;
            float Y = 0;
            float Height = this.Height;
            float Width = this.Width;
            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            Color DrawBackColor = this.BackColor;

            gfx.Clear(this.Parent.BackColor);
            GraphicsPath grPath1 = new GraphicsPath();

            grPath1.AddPolygon(new Point[] { new Point(this.Width, 0), new Point(this.Width, this.Height), new Point(0, this.Height) });

            gfx.FillPath(new SolidBrush(DrawBackColor), grPath1);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseDown = true;
            mousePosition = e.Location;
            startSize = Resizeable.Size;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseDown = false;
            Resizeable.Size = endSize;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        { 
            base.OnMouseMove(e);
            if (mouseDown && Resizeable != null) 
            {
                Size Destination = new Size(startSize.Width + (e.X - mousePosition.X), startSize.Height + (e.Y - mousePosition.Y));
                if (Destination.Width < 402)
                {
                    Destination.Width = 402;
                }
                if (Destination.Width > Resizeable.Parent.Width - Resizeable.Location.X)
                {
                    Destination.Width = Resizeable.Parent.Width;
                }
                if (Destination.Height < 245)
                {
                    Destination.Height = 245;
                }
                if (Destination.Height > Resizeable.Parent.Height - Resizeable.Location.Y)
                {
                    Destination.Height = Resizeable.Parent.Height;
                }
                endSize = Destination;
            }
        }

        public ResizePanel() 
        {
            this.Cursor = Cursors.SizeNWSE;
        }
    }
}
