using System.Diagnostics;
using System.Reflection;

namespace UMeEngine
{
    sealed internal class UMeEngine
    {
        private List<GameComponent> gameComponents = new List<GameComponent>();
        private Stopwatch stopwatch = new Stopwatch();
        private float millisecondsPerTick;
        private bool isPlaying;
        
        public bool IsPlaying => isPlaying;
        
        public UMeEngine(int ticksPerSecond)
        {
            millisecondsPerTick = 1000 / ticksPerSecond;
        }
        
        public void Start()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetLoadableTypes().
            Where(typeof(GameComponent).IsAssignableFrom).ToList())
            {
                GameComponent instance = (GameComponent)Activator.CreateInstance(type);
                gameComponents.Add(instance);
                instance.Start();
            }
            
            stopwatch.Start();
            isPlaying = true;
        }
        
        public void Update()
        {
            if (stopwatch.ElapsedMilliseconds >= millisecondsPerTick)
            {
                Tick();
                stopwatch.Restart();
            }
        }
        
        public void Quit()
        {
            foreach (GameComponent component in gameComponents)
            {
                component.OnDestroy();
            }
        }
        
        private void Tick()
        {
            foreach (GameComponent component in gameComponents)
            {
                component.Update();
            }
        }
    }
}