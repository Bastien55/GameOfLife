using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameOfLife.Utils
{
    public static class Helper
    {
        public static List<Cell> IntoOneDimension(Cell[,] array )
        {

            List<Cell> list = new List<Cell>();

            if (array != null)
            {
                // Get the dimensions of the 2D array
                int rows = array.GetLength(0);
                int cols = array.GetLength(1);

                // Create a 1D array with enough space to store all elements
                Cell[] oneDArray = new Cell[rows * cols];

                // Copy elements from 2D array to 1D array
                int index = 0;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        oneDArray[index++] = array[i, j];
                    }
                }

                list = new List<Cell>(oneDArray);
            }

            return list;
        }
    }
}
