using CommunityToolkit.Mvvm.ComponentModel;
using GameOfLife.Game;
using GameOfLife.Models;
using GameOfLife.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.ViewModel
{
    public class GameViewModel : ObservableObject
    {
        #region Attributes
        private Cell[,] _cells;
        private int _rowCount;
        private int _columnCount;
        #endregion

        #region Properties
        public Cell[,] Cells
        {
            get { return _cells; }
            set 
            {

                SetProperty(ref _cells, value);
                OnPropertyChanged(nameof(Cells1D));
                OnPropertyChanged(nameof(NumberGeneration));
            }
        }

        public int RowCount
        {
            get { return _rowCount; }
            set { SetProperty(ref _rowCount,value); }
        }
        public int ColumnCount
        {
            get { return _columnCount;}
            set { SetProperty(ref _columnCount,value); }
        }

        public ObservableCollection<Cell> Cells1D
        {
            get { return new ObservableCollection<Cell>(Helper.IntoOneDimension(Cells)); }
        }

        public int NumberGeneration { get { return GameManager.Instance.Logical.NumberGeneration; } }
        #endregion

        #region Ctor
        public GameViewModel()
        {
            Cells = GameManager.Instance.Logical.Area;
            RowCount = Cells.GetLength(0);
            ColumnCount = Cells.GetLength(1);
            GameManager.Instance.Logical.OnGenerationChanged += Logical_OnGenerationChanged;
            GameManager.Instance.Start();
        }

        private void Logical_OnGenerationChanged(object? sender, EventArgs e)
        {
            if(sender != null)
            {
                Cells = (Cell[,])sender;
                RowCount = Cells.GetLength(0);
                ColumnCount = Cells.GetLength(1);
            }
                
        }
        #endregion
    }
}
