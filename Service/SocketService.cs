using GameOfLife.Models;
using SocketBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Service
{
    public class SocketService
    {
        #region Singleton
        private readonly static object padlock = new object();
        private static SocketService instance;

        public static SocketService Instance
        {
            get
            {
                if(instance == null)
                {
                    lock(padlock)
                    {
                        instance = new SocketService();
                    }
                }

                return instance;
            }
        }
        #endregion

        public SocketClient Client { get; private set; }

        private SocketService()
        {
            Client = new SocketClient("127.0.0.1", 5555);
        }
    }
}
