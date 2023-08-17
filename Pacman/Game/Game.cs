using UMeEngine;

namespace Pacman
{
    public class Game : Scene2D
    {
        public override bool IsBootScene { get { return true; } }
        
        Grid grid;
        
        public override void CreateGameComponents()
        {
            base.CreateGameComponents();
            
            GameComponents.AddRange(new List<GameComponent>()
            {
                new Player(),
                new Ghost(),
                new Ghost(),
                new Ghost()
            });            
        }
        
        public override void Initialize()
        {
            grid = new Grid(20, 20);
        }
    }
}