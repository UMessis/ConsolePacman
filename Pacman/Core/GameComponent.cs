using System.Numerics;

namespace UMeEngine
{
    public class GameComponent
    {
        public Vector2 Position { get; set; }
        public virtual bool IsStatic { get { return false; } }
        
        public virtual void Start() {}
        public virtual void Update() {}
        public virtual void OnDestroy() {}
    }
}