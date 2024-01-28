namespace UMeEngine
{
    public class Scene
    {
        public List<GameComponent> GameComponents { get; } = new List<GameComponent>();
        public Camera Camera { get; set; }
        public virtual bool IsBootScene { get; }
        
        public virtual void CreateGameComponents() { }
        public virtual void Initialize() { }
    }
}