namespace Peco
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            App app = new();
            app.Run();
        }
    }
}