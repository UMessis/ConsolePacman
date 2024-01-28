namespace UMeEngine
{
    using System.Diagnostics;
    using System.Reflection;

    static internal class UMeEngine
    {
        private static List<GameComponent> staticGameComponents = new List<GameComponent>();
        
        private static Stopwatch stopwatch = new Stopwatch();
        private static float millisecondsPerTick = (float)1000 / Constants.TARGET_FPS;
        
        private static List<Scene> scenes = new List<Scene>();
        private static Scene activeScene;
        
        public static void Start()
        {
            Setup();
            
            GetAllScenes();
            GetAllStaticGameComponents();
            
            stopwatch.Start();
            Update();
        }
        
        private static void Update()
        {
            while (Application.IsRunning)
            {
                if (stopwatch.Elapsed.TotalMilliseconds >= millisecondsPerTick)
                {
                    Time.DeltaTime = (float)stopwatch.Elapsed.TotalMilliseconds / 1000;
                    Tick();
                    stopwatch.Restart();
                }
            }
            
            Quit();
        }
        
        private static void Quit()
        {
            foreach (var component in activeScene.GameComponents)
            {
                component.OnDestroy();
            }
            activeScene.GameComponents.Clear();

            foreach (var component in staticGameComponents)
            {
                component.OnDestroy();
            }
            staticGameComponents.Clear();
        }
        
        private static void Setup()
        {
            Console.SetWindowSize(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT);
            Console.SetBufferSize(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT);
        }

        private static void Tick()
        {
            Console.Title = $"{Constants.WINDOW_TITLE}, FPS: {Math.Ceiling(1000 / stopwatch.Elapsed.TotalMilliseconds)}";
            
            foreach (var component in staticGameComponents)
            {
                component.Update();
            }
            
            foreach (var component in activeScene.GameComponents)
            {
                component.Update();
            }
        }
        
        private static void GetAllScenes()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetLoadableTypes().
            Where(typeof(Scene).IsAssignableFrom).ToList())
            {
                var scene = (Scene)Activator.CreateInstance(type);
                scenes.Add(scene);
                
                if (scene.IsBootScene)
                {
                    activeScene = scene;
                }
            }
            
            if (activeScene == null)
            {
                Debug.WriteLine("There is no boot scene selected");
                return;
            }

            InitializeBootScene();
        }
        
        private static void InitializeBootScene()
        {
            activeScene.CreateGameComponents();
            foreach (var component in activeScene.GameComponents)
            {
                component.Start();
            }
        }
        
        private static void GetAllStaticGameComponents()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetLoadableTypes().
            Where(typeof(GameComponent).IsAssignableFrom).ToList())
            {
                var instance = (GameComponent)Activator.CreateInstance(type);

                if (!instance.IsStatic)
                {
                    continue;
                }
                
                staticGameComponents.Add(instance);
                instance.Start();
            }
        }
    }
}