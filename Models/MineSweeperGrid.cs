using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.CodeDom.Compiler;

namespace MineSweeperDemo.API.Models
{
    public class MineSweeperGrid : IGameGrid
    {
        public int NRows { get; }

        public int NCols { get; }

        public int MineCount { get; }

        public int[,] Grids { get; set; }

        public MineSweeperGrid(int nrows, int ncols, int mineCount)
        {
            this.NRows = nrows;
            this.NCols = ncols;
            this.MineCount = mineCount;
            this.Grids = new int[this.NRows, this.NCols];
        }

        public int[,] GenerateGridData()
        {
            PlaceMines();
            PopulateNumberGrids(); 
            return Grids;
        }

        public void PlaceMines()
        {
            var minePos = GenerateRandomUniquePositions(this.MineCount);
            int currIdx = 0;

            for(int i = 0; i < this.Grids.GetLength(0); i++)
            {
                for(int j = 0; j < this.Grids.GetLength(1); j++)
                {
                    if (minePos.Contains(currIdx))
                    {
                        this.Grids[i, j] = -99; // -99 represents a mine 
                    }
                    currIdx++;
                }
            }
        }

        public void PopulateNumberGrids()
        {
            for(int i=0; i<this.Grids.GetLength(0); i++)
            {
                for(int j=0; j < this.Grids.GetLength(1); j++)
                {
                    if (this.Grids[i, j] != -99)
                    {
                        //getting the adjacent mine count of the non-mine grids
                        this.Grids[i, j] = GetCountIfMineAdjacent((i, j));
                    }
                }
            }
        }

        public int GetCountIfMineAdjacent((int,int) pos)
        {
            int mineCount = 0;
            //Iterate through the sub-grid
            for(int i = pos.Item1-1; i < pos.Item1 + 2; i++)
            {
                for(int j = pos.Item2-1; j < pos.Item2 + 2; j++)
                {
                    if((i,j) != pos) // ignoring the curr index
                    {
                        if(!(i < 0 || j < 0) && !(i >= this.Grids.GetLength(0) || j >= this.Grids.GetLength(1))) //checking if not out of bounds
                        {
                            if (this.Grids[i, j] == -99)
                                mineCount += 1;
                        }
                    }
                }    
            }
            return mineCount;

        }

        public IList<int> GenerateRandomUniquePositions(int count)
        {
            HashSet<int> set = new HashSet<int>();
            IList<int> minePositions = new List<int>();

            Random random = new Random();

            while(true)
            {
                var pos = random.Next(NRows*NCols);

                if (!set.Contains(pos))
                {
                    minePositions.Add(pos);
                    set.Add(pos);
                }

                if (minePositions.Count == count)
                    break;
            }

            return minePositions;
        }
    }
}
