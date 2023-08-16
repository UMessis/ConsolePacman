namespace Pacman
{
    public class Grid
    {
        private Cell[][] cells;
        
        public Grid(int width, int height)
        {
            cells = new Cell[height][];
            
            for (int i = 0; i < height; i++)
            {
                cells[i] = new Cell[width];
            }
        }
    }
}