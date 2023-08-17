using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;

namespace UMeEngine
{
    static internal class UMeEngine
    {
        private static List<GameComponent> staticGameComponents = new List<GameComponent>();
        
        // tick
        private static Stopwatch stopwatch = new Stopwatch();
        private static float millisecondsPerTick;
        
        // scenes
        private static List<Scene2D> scenes = new List<Scene2D>();
        private static Scene2D activeScene;
        
        private static bool isPlaying;
        
        public static bool IsPlaying => isPlaying;
        
        public static void Setup(int ticksPerSecond)
        {
            millisecondsPerTick = 1000 / ticksPerSecond;
        }
        
        public static void Start()
        {
            GetAllScenes();
            GetAllStaticGameComponents();
            stopwatch.Start();
            isPlaying = true;
        }
        
        public static void Update()
        {
            if (stopwatch.ElapsedMilliseconds >= millisecondsPerTick)
            {
                Tick();
                stopwatch.Restart();
            }
        }
        
        public static void Quit()
        {
            foreach (GameComponent component in staticGameComponents)
            {
                component.OnDestroy();
            }
        }
        
        private static void Tick()
        {
            foreach (GameComponent component in staticGameComponents)
            {
                component.Update();
            }
        }
        
        private static void GetAllScenes()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetLoadableTypes().
            Where(typeof(Scene2D).IsAssignableFrom).ToList())
            {
                Scene2D scene = (Scene2D)Activator.CreateInstance(type);
                scenes.Add(scene);
                
                if (scene.IsBootScene)
                {
                    activeScene = scene;
                }
            }
            
            if (activeScene is null)
            {
                Debug.WriteLine("There is no boot scene selected");
            }           
        }
        
        private static void GetAllStaticGameComponents()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetLoadableTypes().
            Where(typeof(GameComponent).IsAssignableFrom).ToList())
            {
                GameComponent instance = (GameComponent)Activator.CreateInstance(type);
                
                if (!instance.IsStatic) continue;
                
                staticGameComponents.Add(instance);
                instance.Start();
            }
        }
    }
}