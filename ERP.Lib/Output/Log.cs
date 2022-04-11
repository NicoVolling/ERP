using System.Text;

namespace ERP.BaseLib.Output
{
    public static class Log
    {
        public static bool SaveOutput = false;
        private static FileStream _stream;

        static Log()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public static string Prefix { get; set; } = "";

        public static void WriteLine(Object value, ConsoleColor Color = ConsoleColor.White) => WriteLine(value.ToString(), Color);

        public static void WriteLine(string value, ConsoleColor Color = ConsoleColor.White)
        {
            if (SaveOutput)
            {
                string pre = "";
                if (Prefix != "")
                {
                    pre = $"[{Prefix}]";
                }
                string text = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss:fff}>{pre}>>{value}\n";

                if (_stream == null) { CreateNewFileStream(); }
                if (_stream is FileStream FS)
                {
                    byte[] data = new UTF8Encoding(true).GetBytes(text);

                    FS.Write(data, 0, data.Length);
                    FS.FlushAsync();
                }
            }

            System.Console.ForegroundColor = ConsoleColor.DarkGray;
            System.Console.Write($"{DateTime.Now:dd.MM.yyyy HH:mm:ss:fff}");

            System.Console.ForegroundColor = ConsoleColor.Gray;
            System.Console.Write(">");

            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.Write("[");

            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.Write(Prefix);

            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.Write("]");

            System.Console.ForegroundColor = ConsoleColor.Gray;
            System.Console.Write(">>");

            System.Console.ForegroundColor = Color;
            System.Console.Write(value + "\n");
        }

        private static void CreateNewFileStream()
        {
            try
            {
                string basedir = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Log");
                if (!System.IO.Directory.Exists(basedir)) { System.IO.Directory.CreateDirectory(basedir); }
                if (Prefix != "")
                {
                    basedir = System.IO.Path.Combine(basedir, Prefix);
                    if (!System.IO.Directory.Exists(basedir)) { System.IO.Directory.CreateDirectory(basedir); }
                }
                _stream = System.IO.File.OpenWrite(System.IO.Path.Combine(basedir, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff") + ".txt"));
            }
            catch
            {
                _stream = null;
            }
        }
    }
}