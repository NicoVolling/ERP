using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Windows.WF.Base
{
    public static class ControlHelper
    {
        public static PointF AlignContent(ContentAlignment Align, Size Size, Padding Padding, SizeF ContentSize)
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