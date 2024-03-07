using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Models
{
    public class Rule
    {
        /// <summary>
        /// Require number for giving birth to a cell
        /// </summary>
        public int Alive { get; set; }

        /// <summary>
        /// Require number for staying alive for a cell
        /// </summary>
        public int Stay { get; set; }

        /// <summary>
        /// Constructor of the game rule
        /// </summary>
        /// <param name="rule">We know the format is XAXS where X is a number example : 3A2S</param>
        public Rule(string rule)
        {
            Alive = int.Parse(rule[0].ToString());
            Stay = int.Parse(rule[2].ToString());
        }

        public override string ToString()
        {
            return $"{Alive}A{Stay}S";
        }
    }
}
