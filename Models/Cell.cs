using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Models
{
    /// <summary>
    /// Model that represent an instance of a cell
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Indicate if this cell is alive
        /// </summary>
        public bool IsAlive { get; set; }

        /// <summary>
        /// Represent the X position of the cell
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Represent the Y position of the cell
        /// </summary>
        public int Y { get; set; }

        public Cell()
        {
            
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
