using ERP.Reporting.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Reporting.Designer.Controls
{
    internal class DesignerPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Textfield Tf = new Textfield();
            Tf.Width = 50;
            Tf.Height = 50;
            Tf.Fontfamily = "Arial";
            Tf.Fontsize = 12;
            Tf.Text = "Hallo das ist ein {Value}";
            Tf.DataContext = new Dictionary<string, string>() { { "Value", "Test" } };
            Tf.Padding = new Objects.Padding(10);
            Tf.Draw(e.Graphics);
        }
    }
}
