using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameOfLife.Events
{
    public class EventService
    {
        #region Singleton
        private static readonly object padlock = new object();
        private static EventService _instance;

        public static EventService Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(padlock)
                    {
                        _instance = new EventService();
                    }
                }

                return _instance;
            }
        }
        #endregion

        #region Events Declaration
        public event EventHandler OnShowControl;
        #endregion

        #region Events Invocations
        /// <summary>
        /// Event which allow to show the control for reload game request
        /// </summary>
        public void RaiseShowControl()
        {
            if(OnShowControl != null)
            {
                OnShowControl.Invoke(null, EventArgs.Empty);
            }
        }
        #endregion
    }
}
