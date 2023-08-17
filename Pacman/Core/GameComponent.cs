namespace UMeEngine
{
    public class GameComponent
    {
        public (float x, float y) Position { get; set; }
        
        public virtual bool IsStatic { get { return false; } }
        
        public virtual void Start() {}
        public virtual void Update() {}
        public virtual void OnDestroy() {}
    }
}