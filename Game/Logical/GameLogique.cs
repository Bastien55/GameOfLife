using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Game.Logical
{
    public class GameLogique
    {
        private Cell[,] _area;

        private int _numberGeneration = 0;

        public Cell[,] Area
        {
            get { return _area; }
            set 
            {
                _area = value;
                OnGenerationChanged?.Invoke(_area, EventArgs.Empty);   
            }
        }

        public int NumberGeneration
        {
            get { return _numberGeneration; }
            set 
            {
                _numberGeneration = value;
                if(_numberGeneration == 10000)
                    OnMaxGeneration?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler OnMaxGeneration;

        public event EventHandler OnGenerationChanged;

        public GameLogique(Cell[,] cells)
        {
            Area = cells;
        }

        #region Methods

        public void SimulateGeneration()
        {
            if(NumberGeneration == 0)
                Generate_Cells();

            Cell[,] currentGeneration = Area;
            int rows = currentGeneration.GetLength(0);
            int cols = currentGeneration.GetLength(1);
            Cell[,] newGeneration = new Cell[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newGeneration[i, j] = new Cell(i, j);

                    int liveNeighbors = CountLiveNeighbors(currentGeneration, i, j);

                    // Apply the rules of the Game of Life
                    if (currentGeneration[i, j].IsAlive)
                    {
                        newGeneration[i, j].IsAlive = liveNeighbors == 2 || liveNeighbors == 3;
                    }
                    else
                    {
                        newGeneration[i, j].IsAlive = liveNeighbors == 3;
                    }
                }
            }

            Area = newGeneration;
            NumberGeneration++;
        }

        public void Generate_Cells()
        {
            Random random = new Random();
            if(Area != null)
            {
                for(int i = 0; i < Area.GetLength(0); i++)
                {
                    for(int j = 0; j < Area.GetLength(1); j++)
                    {
                        var r = random.Next(0, 2);
                        Area[i, j].IsAlive = r == 1;
                    }
                }
            }
        }

        private int CountLiveNeighbors(Cell[,] grid, int row, int col)
        {
            int liveNeighbors = 0;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int neighborRow = row + i;
                    int neighborCol = col + j;

                    // Check boundaries to avoid out-of-bounds access
                    if (neighborRow >= 0 && neighborRow < rows && neighborCol >= 0 && neighborCol < cols)
                    {
                        if (grid[neighborRow, neighborCol].IsAlive)
                            liveNeighbors++;
                    }
                }
            }

            return liveNeighbors;
        }
        #endregion
    }
}
