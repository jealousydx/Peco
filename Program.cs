namespace Peco
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Settings.Init();

            App app = new();
            app.Run();
        }
    }
}