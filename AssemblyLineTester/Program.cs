using AssemblyLineTester.Data;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views;
using System.Text;


namespace AssemblyLineTester
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Register code pages provider so PdfSharp and other libraries can use code page encodings (e.g., 1252)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //initalize the database
            DbInitialize.Initialize();

            Application.Run(new LoginForm());
        }
    }
}