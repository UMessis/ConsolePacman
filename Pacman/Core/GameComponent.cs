namespace UMeEngine
{
    public class GameComponent
    {
        public virtual bool IsStatic { get; }
        
        public virtual void Start() {}
        public virtual void Update() {}
        public virtual void OnDestroy() {}
    }
}