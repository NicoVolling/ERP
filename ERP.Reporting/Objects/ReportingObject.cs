using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Reporting.Objects
{
    public abstract class ReportingObject
    {
        public double Width { get; set; }

        public double Height { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double AbsoluteX 
        {
            get 
            {
                if (Parent != null) 
                { 
                    return Parent.AbsoluteX + Parent.Padding.Left + X; 
                }
                return X;
            }
        }
        public double AbsoluteY 
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.AbsoluteY + Parent.Padding.Left + Y;
                }
                return X;
            }
        }

        public double AbsoluteWidth 
        {
            get 
            {
                if (Parent != null)
                {
                    double wdth = Parent.Width - Parent.Padding.Right - Parent.Padding.Left - X;
                    if (wdth > Width) { wdth = Width; }
                    return wdth;
                }
                return Width;
            }
        }

        public double AbsoluteHeight
        {
            get
            {
                if (Parent != null)
                {
                    double hght = Parent.Height - Parent.Padding.Bottom - Parent.Padding.Top - Y;
                    if (hght > Width) { hght = Width; }
                    return hght;
                }
                return Height;
            }
        }

        public ReportingObject Parent { get; set; }

        public string Fontfamily { get; set; }

        public double Fontsize { get; set; }

        public Padding Padding { get; set; }

        public abstract void Draw(XGraphics Gfx);

        public abstract void Draw(Graphics Gfx);

        public Dictionary<string, string> DataContext { get; set; } = new Dictionary<string, string>();

        public void FillData(Dictionary<string, string> DataContext) 
        {
            if(DataContext == null) 
            {
                throw new ArgumentNullException(nameof(DataContext));
            }
            this.DataContext = DataContext;
        }

        protected string PrepareText(string Text) 
        {
            foreach(KeyValuePair<string, string> Kvp in DataContext) 
            {
                Text = Text.Replace(@"\{", @"\´#+53");
                Text = Text.Replace("{" + Kvp.Key + "}", Kvp.Value);
                Text = Text.Replace(@"\´#+53", @"{");
            }
            return Text;
        }
    }
}
