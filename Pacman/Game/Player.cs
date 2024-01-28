using System.Numerics;
using UMeEngine;

namespace Pacman
{
    public class Player : GameComponent
    {
        private float moveSpeed = 3;
        private char visual = '#';
        private Vector2 direction = new Vector2(0, 0);
        
        public char Visual => visual;
        
        public override void Update()
        {
            GetInputs();
            MovePlayer();
            DisplayPosition();
        }
        
        private void GetInputs()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        direction = new Vector2(1, 0);
                        break;
                    case ConsoleKey.S:
                        direction = new Vector2(-1, 0);
                        break;
                    case ConsoleKey.D:
                        direction = new Vector2(0, 1);
                        break;
                    case ConsoleKey.A:
                        direction = new Vector2(0, -1);
                        break;
                }
            }
        }
        
        private void MovePlayer()
        {
            Position += direction * Time.DeltaTime * moveSpeed;
        }
        
        private void DisplayPosition()
        {
            Console.Clear();
            Console.WriteLine("x:" + Position.X.ToString("n2") + "\n" +
            "y:" + Position.Y.ToString("n2"));
        }
    }
}