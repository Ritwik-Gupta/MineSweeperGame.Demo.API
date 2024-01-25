using MineSweeperDemo.API.Models;

namespace MineSweeperDemo.API.Service
{
    public class MineSweeperGameService : IGameService
    {
        public void Start()
        {
            Console.Write("Starting the game...");
        }

        public void Reset()
        {
            Console.WriteLine("Resetting the board...");
        }

        public int[,] GenerateGridData(int nrows, int ncols, int mines)
        {
            IGameGrid grid = new MineSweeperGrid(nrows, ncols, mines);
            
            return grid.GenerateGridData();
        }
    }
}
