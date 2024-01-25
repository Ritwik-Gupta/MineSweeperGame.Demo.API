using System.CodeDom.Compiler;

namespace MineSweeperDemo.API.Models
{
    public interface IGameGrid
    {
        int[,] GenerateGridData();
    }
}
