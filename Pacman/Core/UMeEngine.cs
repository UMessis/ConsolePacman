using System.Diagnostics;
using System.Reflection;

namespace UMeEngine
{
    static internal class UMeEngine
    {
        private static List<GameComponent> staticGameComponents = new List<GameComponent>();
        
        private static Stopwatch stopwatch = new Stopwatch();
        private static float millisecondsPerTick = (float)1000 / Constants.TARGET_FPS;
        
        private static List<Scene2D> scenes = new List<Scene2D>();
        private static Scene2D activeScene;
        
        private static bool isPlaying;
        
        public static bool IsPlaying => isPlaying;
        
        public static void Start()
        {
            Setup();
            
            GetAllScenes();
            GetAllStaticGameComponents();
            
            stopwatch.Start();
            isPlaying = true;
        }
        
        public static void Update()
        {
            if (stopwatch.Elapsed.TotalMilliseconds >= millisecondsPerTick)
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
        
        private static void Setup()
        {
            Console.SetWindowSize(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT);
            Console.SetBufferSize(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT);
        }
        
        private static void Tick()
        {
            Console.Title = $"{Constants.WINDOW_TITLE}, FPS: {Math.Ceiling(1000 / stopwatch.Elapsed.TotalMilliseconds)}";
            
            foreach (GameComponent component in staticGameComponents)
            {
                component.Update();
            }
            
            foreach (GameComponent component in activeScene.GameComponents)
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
                    activeScene.CreateGameComponents();
                    
                    foreach (GameComponent component in activeScene.GameComponents)
                    {
                        component.Start();
                    }
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