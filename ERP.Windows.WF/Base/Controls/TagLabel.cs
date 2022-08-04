using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ERP.Windows.WF.Base.Controls
{
    public class TagLabel : Label
    {
        private bool _AutoWidth = true;

        [Category("Layout")]
        public bool AutoWidth
        { get => _AutoWidth; set { _AutoWidth = value; this.Refresh(); } }

        public override void Refresh()
        {
            base.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float X = Padding.Left;
            float Y = Padding.Top;
            float Height = this.Height - Padding.Top - Padding.Bottom;
            float Width = this.Width - Padding.Left - Padding.Right;
            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            if (TextAlign == ContentAlignment.BottomLeft || TextAlign == ContentAlignment.MiddleLeft || TextAlign == ContentAlignment.TopLeft)
            {
                sf.Alignment = StringAlignment.Near;
            }
            else if (TextAlign == ContentAlignment.BottomRight || TextAlign == ContentAlignment.MiddleRight || TextAlign == ContentAlignment.TopRight)
            {
                sf.Alignment = StringAlignment.Far;
            }
            if (TextAlign == ContentAlignment.TopLeft || TextAlign == ContentAlignment.TopRight || TextAlign == ContentAlignment.TopCenter)
            {
                sf.LineAlignment = StringAlignment.Near;
            }
            else if (TextAlign == ContentAlignment.BottomRight || TextAlign == ContentAlignment.BottomRight || TextAlign == ContentAlignment.BottomCenter)
            {
                sf.LineAlignment = StringAlignment.Far;
            }

            gfx.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(X, Y, Width, Height), sf);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            float X = Padding.Left;
            float Y = Padding.Top;
            float Height = this.Height - Padding.Top - Padding.Bottom;
            float Width = this.Width - Padding.Left - Padding.Right;
            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            if (AutoWidth)
            {
                Width = gfx.MeasureString(Text, Font).Width + 2;
            }
            Color DrawBackColor = this.BackColor;

            GraphicsPath grPath1 = new GraphicsPath();

            grPath1.AddPie(X, Y, Height, Height, 90, 180);
            grPath1.AddPie(Width - Height, Y, Height, Height, 270, 180);
            grPath1.AddRectangle(new RectangleF(X + (Height / 2), Y, Width - Height - Padding.Left, Height));

            gfx.Clear(this.Parent.BackColor);
            gfx.FillPath(new SolidBrush(DrawBackColor), grPath1);
        }
    }
}