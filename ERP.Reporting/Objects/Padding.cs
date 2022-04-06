using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Reporting.Objects
{
    public struct Padding
    {
        public double Left { get; set; }

        public double Top { get; set; }

        public double Right { get; set; }

        public double Bottom { get; set; }

        public Padding(double All) 
        {
            this.Left = All;
            this.Top = All;
            this.Right = All;
            this.Bottom = All;
        }

        public Padding(double Left, double Right, double Top, double Bottom) 
        {
            this.Left = Left;
            this.Top = Top;
            this.Right = Right;
            this.Bottom = Bottom;
        }

        public Padding() 
        {
            this.Left = 0;
            this.Top = 0;
            this.Right = 0;
            this.Bottom = 0;
        }
    }
}
