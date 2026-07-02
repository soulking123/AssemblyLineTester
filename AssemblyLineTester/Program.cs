using AssemblyLineTester.Views;
using AssemblyLineTester.Data;


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

            //initalize the database
            DbInitialize.Initialize();

            Application.Run(new LoginForm());
        }
    }
}