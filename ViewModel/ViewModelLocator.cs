using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.ViewModel
{
    public class ViewModelLocator
    {
        #region Singleton
        private static readonly object padlock = new object();
        private static ViewModelLocator _instance;

        public static ViewModelLocator Instance
        {
            get
            {
                if( _instance == null)
                {
                    lock(padlock)
                    {
                        _instance = new ViewModelLocator();
                    }
                }

                return _instance;
            }
        }

        #endregion

        public UserViewModel UserVM { get; set; }

        public ViewModelLocator()
        {
            UserVM = new UserViewModel();
        }
    }
}
