using System.Drawing.Drawing2D;

namespace ERP.Windows.WF.Base.Controls
{
    public class StatusLed : Control
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            float Height = this.Height;
            float Width = this.Width;
            if (Width < Height)
            {
                Height = Width;
            }
            else if (Height < Width)
            {
                Width = Height;
            }

            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            Color DrawForeColor = this.ForeColor;

            GraphicsPath grPath2 = new GraphicsPath();

            grPath2.AddPie(Padding.Left + ((this.Width - Width) / 2), Padding.Top + ((this.Height - Height) / 2), Width - Padding.Left - Padding.Right, Height - Padding.Top - Padding.Bottom, 0, 360);

            gfx.Clear(this.Parent.BackColor);
            gfx.FillPath(new SolidBrush(DrawForeColor), grPath2);
        }
    }
}