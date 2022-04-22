using ERP.Test.Client.WindowsApp.Windows;
using ERP.Windows.WF.Base;
using ERP.Windows.WF.Binding.Forms;

namespace ERP.Test.Client.WindowsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new PersonForm());
        }
    }
}