namespace Peco
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            _ = new Mutex(true, "Global\\Peco", out bool isFree);

            if (!isFree)
            {
                Alert.Error("The program has already running");
                return;
            }

            ApplicationConfiguration.Initialize();

            App app = new();
            app.Run();
        }
    }
}