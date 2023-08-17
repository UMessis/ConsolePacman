namespace UMeEngine
{
    public class Scene2D
    {
        private List<GameComponent> gameComponents = new List<GameComponent>();
        
        public List<GameComponent> GameComponents => gameComponents;
        
        public virtual bool IsBootScene { get; }
        
        public virtual void CreateGameComponents() { }
        
        public virtual void Initialize() { }
    }
}