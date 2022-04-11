using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ERP.Client.WindowsForms.Controls.Base
{
    public class RoundImageButton : Control
    {
        private Image _Image;

        private bool IsMouseDown;

        private bool IsMouseInBounds;

        [Category("Darstellung")]
        public Image Image
        { get => _Image; set { _Image = value; this.Refresh(); } }

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
            float X = this.Width * 0.15f;
            float Y = this.Height * 0.15f;
            float Height = this.Height * 0.7f;
            float Width = this.Width * 0.7f;
            Graphics gfx = e.Graphics;
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            if (Image != null)
            {
                gfx.DrawImage(Image, X, Y, Width, Height);
            }
            else
            {
                PointF StringPoint = AlignContent(ContentAlignment.MiddleCenter, Size, Padding, gfx.MeasureString(Text, Font));
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

        private PointF AlignContent(ContentAlignment Align, Size Size, Padding Padding, SizeF ContentSize)
        {
            PointF Location;

            switch (Align)
            {
                case ContentAlignment.BottomLeft:
                    Location = new PointF(Padding.Left, Size.Height - Padding.Bottom - ContentSize.Height);
                    break;

                case ContentAlignment.MiddleLeft:
                    Location = new PointF(Padding.Left, Size.Height / 2 - ContentSize.Height / 2);
                    break;

                case ContentAlignment.TopCenter:
                    Location = new PointF(Size.Width / 2 - ContentSize.Width / 2, Padding.Top);
                    break;

                case ContentAlignment.BottomCenter:
                    Location = new PointF(Size.Width / 2 - ContentSize.Width / 2, Size.Height - Padding.Bottom - ContentSize.Height);
                    break;

                case ContentAlignment.MiddleCenter:
                    Location = new PointF(Size.Width / 2 - ContentSize.Width / 2, Size.Height / 2 - ContentSize.Height / 2);
                    break;

                case ContentAlignment.TopRight:
                    Location = new PointF(Size.Width - Padding.Right - ContentSize.Width, Padding.Top);
                    break;

                case ContentAlignment.MiddleRight:
                    Location = new PointF(Size.Width - Padding.Right - ContentSize.Width, Size.Height / 2 - ContentSize.Height / 2);
                    break;

                case ContentAlignment.BottomRight:
                    Location = new PointF(Size.Width - Padding.Right - ContentSize.Width, Size.Height - Padding.Bottom - ContentSize.Height);
                    break;

                case ContentAlignment.TopLeft:
                    Location = new PointF(Padding.Left, Padding.Top);
                    break;

                default:
                    Location = new PointF(0, 0);
                    break;
            }
            return Location;
        }
    }
}