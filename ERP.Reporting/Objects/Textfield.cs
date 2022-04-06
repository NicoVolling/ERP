using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Reporting.Objects
{
    public class Textfield : ReportingObject
    {

        public string Text { get; set; }

        public override void Draw(XGraphics Gfx)
        {
            Gfx.DrawString(PrepareText(Text), new XFont(Fontfamily, Fontsize), XBrushes.Black, new XRect(AbsoluteX, AbsoluteY, AbsoluteWidth, AbsoluteHeight));
        }

        public override void Draw(Graphics Gfx)
        {
            Gfx.DrawString(PrepareText(Text), new Font(new FontFamily(Fontfamily), (int)Fontsize, FontStyle.Regular), Brushes.Black, new Rectangle((int)AbsoluteX, (int)AbsoluteY, (int)AbsoluteWidth, (int)AbsoluteHeight));
        }
    }
}
