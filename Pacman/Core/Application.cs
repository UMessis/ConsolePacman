namespace UMeEngine
{
    public static class Application
    {
        public static bool IsRunning { get; private set; }
        
        public static void Start()
        {
            IsRunning = true;
            UMeEngine.Start();
        }
        
        public static void Quit()
        {
            IsRunning = false;
        }
    }
}