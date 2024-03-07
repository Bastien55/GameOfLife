using SocketBackend.Messages;
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
        public event EventHandler<UserMessage> OnShowControl;
        public event EventHandler<UserMessage> OnErrorLogin;
        #endregion

        #region Events Invocations
        /// <summary>
        /// Event which allow to show the control for reload game request
        /// </summary>
        public void RaiseShowControl(UserMessage msg)
        {
            if(OnShowControl != null)
            {
                OnShowControl.Invoke(null, msg);
            }
        }

        /// <summary>
        /// Event which allow to display an error msg
        /// </summary>
        /// <param name="msg"></param>
        public void RaiseErrorLogin(UserMessage msg, string ErrorMsg)
        {
            msg.ContentMessage = ErrorMsg;
            OnErrorLogin?.Invoke(null, msg);
        }
        #endregion
    }
}
