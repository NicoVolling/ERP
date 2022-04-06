using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Reporting
{
    public class TestClass
    {
        public void TestMethod()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Consolas", 14);

            gfx.DrawString("Hallo", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height));

            document.Save(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Text.pdf"));
        }
    }
}
