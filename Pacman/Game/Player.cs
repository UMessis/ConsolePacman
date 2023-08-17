using UMeEngine;

namespace Pacman
{
    public class Player : GameComponent
    {
        public override bool IsStatic { get { return false; } }
        
        private int test = 0;
        
        public override void Update()
        {
            Console.Clear();
            Console.WriteLine(test++);
        }
    }
}