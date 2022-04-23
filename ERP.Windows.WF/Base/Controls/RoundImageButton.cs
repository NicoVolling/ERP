using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Windows.WF.Base.Controls
{
    public class RoundImageButton : Control
    {
        private Image _Image;

        [Category("Darstellung")]
        public Image Image
        { get => _Image; set { _Image = value; this.Refresh(); } }

        protected bool IsMouseDown { get; private set; }
        protected bool IsMouseInBounds { get; private set; }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsMouseDown = true;
            this.Refresh();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            IsMouseInBounds = true;
            this.Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsMouseInBounds = false;
            this.Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsMouseDown = false;
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float Height = this.Height * 0.7f;
            float Width = this.Width * 0.7f;
            if (Width > Height) { Width = Height; }
            if (Height > Width) { Height = Width; }
            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            if (Image != null)
            {
                PointF Point = ControlHelper.AlignContent(ContentAlignment.MiddleCenter, Size, Padding, new SizeF(Width, Height));
                gfx.DrawImage(Image, Point.X, Point.Y, Width, Height);
            }
            else
            {
                PointF StringPoint = ControlHelper.AlignContent(ContentAlignment.MiddleCenter, Size, Padding, gfx.MeasureString(Text, Font));
                gfx.DrawString(Text, Font, new SolidBrush(ForeColor), StringPoint);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            float X = 0;
            float Y = 0;
            float Height = this.Height;
            float Width = this.Width;
            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            Color DrawBackColor = this.Parent.BackColor;
            if (IsMouseInBounds)
            {
                DrawBackColor = Color.FromArgb(DrawBackColor.A,
                    DrawBackColor.R > 20 ? DrawBackColor.R - 20 : DrawBackColor.R + 20,
                    DrawBackColor.G > 20 ? DrawBackColor.G - 20 : DrawBackColor.G + 20,
                    DrawBackColor.B > 20 ? DrawBackColor.B - 20 : DrawBackColor.B + 20
                    );
            }
            if (IsMouseDown)
            {
                DrawBackColor = this.BackColor;
                DrawBackColor = Color.FromArgb(DrawBackColor.A,
                    DrawBackColor.R > 30 ? DrawBackColor.R - 30 : DrawBackColor.R + 30,
                    DrawBackColor.G > 30 ? DrawBackColor.G - 30 : DrawBackColor.G + 30,
                    DrawBackColor.B > 30 ? DrawBackColor.B - 30 : DrawBackColor.B + 30
                    );
            }

            gfx.Clear(this.Parent.BackColor);
            GraphicsPath grPath1 = new GraphicsPath();

            grPath1.AddPie(X, Y, Height, Height, 90, 180);
            grPath1.AddPie(Width - Height, 0, Height, Height, 270, 180);
            grPath1.AddRectangle(new RectangleF(Height / 2, Y, Width - Height, Height));

            gfx.FillPath(new SolidBrush(DrawBackColor), grPath1);
        }
    }
}