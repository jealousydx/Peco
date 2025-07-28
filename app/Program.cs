namespace Peco.app
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            _ = new Mutex(true, "Global\\Peco", out bool isFree);

            if (!isFree)
            {
                Alert.Error("Peco.exe is already running");
                return;
            }

            ApplicationConfiguration.Initialize();

            App.Run();
        }
    }
}