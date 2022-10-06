using Tehtava_15.GameStructure;

namespace Tehtava_15
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            MainMenu view = new MainMenu();
            Controller _controller = new(view);
            Application.Run(view);
        }
    }
}