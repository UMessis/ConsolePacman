namespace UMeEngine
{
    public class Scene2D
    {
        private List<GameComponent> gameComponents = new List<GameComponent>();
        
        public virtual bool IsBootScene { get; }
        
        public virtual void CreateGameComponents() { }
    }
}