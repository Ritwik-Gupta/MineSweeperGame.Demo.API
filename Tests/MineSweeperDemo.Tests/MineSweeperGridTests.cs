using MineSweeperDemo.API.Models;

namespace MineSweeperDemo.Tests
{
    public class MineSweeperGridTests
    {

        [Fact]
        public void GenerateGrid_should_return_grids()
        {
            //Arrange
            int nrows = 5;
            int ncols = 5;
            int mines = 9;
            var grid = new MineSweeperGrid(nrows, ncols, mines);

            //Act
            var actual_grid = grid.Grids;

            //Assert
            bool res1 = actual_grid.GetType() == typeof(int[,]);
            bool res2 = actual_grid.GetLength(0) == nrows;
            bool res3 = actual_grid.GetLength(1) == ncols;

            Assert.True(res1 && res2 && res3);
        }

        [Fact]
        public void GenerateRandomUniquePositions_should_return_unique_and_distict_integers_within_range()
        {
            //Arrange
            int nrows = 5;
            int ncols = 5;
            int mines = 9;
            int lower_range = 0;
            int upper_range = nrows * ncols - 1;
            var grid = new MineSweeperGrid(nrows, ncols, mines);

            //Act
            var positions = grid.GenerateRandomUniquePositions(mines);

            //Assert
            Assert.True(positions.Distinct().Count() == mines && positions.All(x => x >= lower_range && x <= upper_range));
        }

        [Fact]
        public void GetCountIfMineAdjacent_should_return_count_of_mines_adjacent_to_the_current_grid()
        {
            //Arrange
            var gridObj = new MineSweeperGrid(3, 3, 3);
            int[,] grids = new int[3, 3]
            {
                { -99, 0  , -99 },
                { 0  , 0  , 0   },
                { 0  , -99, 0   }
            };
            gridObj.Grids = grids;

            int[,] expected_grids = new int[3, 3]
            {
                { -99, 2  , -99 },
                { 2  , 3  , 2   },
                { 1  , -99, 1   }
            };


            //Act
            bool res = false;
            int flag = 0;
            foreach (var trow in Enumerable.Range(0, grids.GetLength(0)))
            {
                foreach (var tcol in Enumerable.Range(0, grids.GetLength(1)))
                {
                    if (grids[trow, tcol] != -99) //skipping the mines
                    {
                        var val = gridObj.GetCountIfMineAdjacent((trow, tcol));
                        if (val != expected_grids[trow, tcol])
                        {
                            res = false;
                            flag = 1;
                            break;
                        }
                        else
                        {
                            res = true;
                        }
                    }
                }
            }

            //Assert
            Assert.True(res && flag == 0);
        }

        [Fact]
        public void PlaceMines_should_place_mines_randomly_in_grids()
        {
            //Arrange
            int nrows = 5;
            int ncols = 5;
            int mines = 9;
            var grid = new MineSweeperGrid(nrows, ncols, mines);

            int expected_mines = 9;
            int actual_mines = 0;

            //Act
            grid.PlaceMines();
            foreach (var trow in Enumerable.Range(0, grid.Grids.GetLength(0)))
            {
                foreach (var tcol in Enumerable.Range(0, grid.Grids.GetLength(1)))
                {
                    if (grid.Grids[trow, tcol] == -99)
                        actual_mines += 1;
                }
            }

            //Assert
            Assert.Equal(expected_mines, actual_mines);
        }
    }
}