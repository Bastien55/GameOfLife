using GameOfLife.Game.Logical;
using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Game
{
    public class GameManager
    {
        #region Constant
        const int NUMBER_OF_ROWS = 20;
        const int NUMBER_OF_COLUMN = 20;
        #endregion

        #region Singleton
        private static readonly object padlock = new object();
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (padlock)
                    {
                        _instance = new GameManager();
                    }
                }
                return _instance;
            }
        }
        #endregion

        private bool GameIsAlive;

        public GameLogique Logical { get; set; }

        private GameManager()
        {
            Logical = new GameLogique(InitArea());
            Logical.OnMaxGeneration += Logical_OnMaxGeneration;
        }

        /// <summary>
        /// Method for the initialization of the area
        /// </summary>
        /// <returns></returns>
        public Cell[,] InitArea()
        {
            Cell[,] Area = new Cell[NUMBER_OF_ROWS, NUMBER_OF_COLUMN];

            for (int x = 0; x < NUMBER_OF_ROWS; x++)
            {
                for (int y = 0; y < NUMBER_OF_COLUMN; y++)
                {
                    Area[x, y] = new Cell(x, y);
                }
            }

            return Area;
        }

        /// <summary>
        /// Event which allow to stop the game whether we hit the max generation number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logical_OnMaxGeneration(object? sender, EventArgs e)
        {
            Stop();
        }

        public void Start()
        {
            GameIsAlive = true;
            Thread thread = new Thread(() =>
            {
                while(GameIsAlive)
                {
                    Logical.SimulateGeneration();
                    Thread.Sleep(TimeSpan.FromSeconds(0.25));
                }
            });

            thread.Start();
        }

        public void Stop()
        {
            GameIsAlive = false;
        }

        /// <summary>
        /// Method which allow to restart the game
        /// </summary>
        public void ReloadGame()
        {
            Stop();
            Logical.NumberGeneration = 0;
            Logical.Area = InitArea();
            Start();
        }
    }
}
