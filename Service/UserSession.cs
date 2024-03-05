using GameOfLife.Events;
using GameOfLife.Game;
using GameOfLife.Models;
using GameOfLife.ViewModel;
using SocketBackend.Enumeration;
using SocketBackend.Messages;
using SocketBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Service
{
    public class UserSession
    {
        #region Singleton
        private static readonly object padlock = new object();
        private static UserSession _instance = null;

        public static UserSession Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(padlock)
                    {
                        _instance = new UserSession();
                    }
                }

                return _instance;
            }
        }
        #endregion

        public UserSession()
        {
            SocketService.Instance.Client.OnUserMessageReceived += Handler_OnMessageReceived;
        }

        private Rule _rule = new Rule("3A2S");

        #region Properties
        public User CurrentUser { get; set; } = new User();

        public Rule CurrentRule
        {
            get { return _rule; }
            set 
            { 
                _rule = value;
                ViewModelLocator.Instance.UserVM.Rule = _rule.ToString();
            }
        }

        public bool IsLoggedIn { get; set; } = false;
        #endregion

        public void UserConnected()
        {
            CurrentRule = new Rule(CurrentUser.Rule);
            IsLoggedIn = true;
            ViewModel.ViewModelLocator.Instance.UserVM.Update();
            GameManager.Instance.Start();
        }

        private void Handler_OnMessageReceived(object? sender, UserMessage e)
        {
            try
            {
                if (e != null)
                {
                    switch(e.TypeMessage)
                    {
                        case SocketBackend.Enumeration.TypeMessage.GAME_REPLAY:
                            EventService.Instance.RaiseShowControl(e);
                            break;
                        case TypeMessage.USER_REGISTERED:
                        case TypeMessage.USER_VALID_CONNECTION:
                            InitCurrentUser(e);
                            break;
                        case TypeMessage.USER_INVALID_CONNECTION:
                            EventService.Instance.RaiseErrorLogin(e, "Erreur de connection");
                            break;
                        case TypeMessage.USER_ALREADY_EXIST:
                            EventService.Instance.RaiseErrorLogin(e, "Utilisateur déjà existant");
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void InitCurrentUser(UserMessage msg)
        {
            if (msg == null)
                return;

            CurrentUser = msg.UserModel;
            UserConnected();
        }
    }
}
