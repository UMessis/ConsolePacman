using UMeEngine;

namespace Pacman
{
    public class Player : GameComponent
    {
        private int test = 0;
        
        private string visual = "#";
        
        public override void Update()
        {
            Console.Clear();
            Console.WriteLine(test++);
        }
    }
}