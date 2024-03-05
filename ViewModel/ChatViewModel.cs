using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOfLife.Events;
using GameOfLife.Game;
using GameOfLife.Service;
using SocketBackend;
using SocketBackend.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameOfLife.ViewModel
{
    public partial class ChatViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Message> messages = new ObservableCollection<Message>();

        private string inputMessage;

        /// <summary>
        /// Message d'entrée
        /// </summary>
        public string InputMessage
        {
            get { return inputMessage; }
            set { SetProperty(ref inputMessage, value); }
        }

        public ChatViewModel()
        {
            Task.Run(async() => await SocketService.Instance.Client.ConnectAsync()).Wait(10);

            SocketService.Instance.Client.OnMessageReceived += Client_OnMessageReceived;
        }

        private void Client_OnMessageReceived(object? sender, Message e)
        {
            switch(e.TypeMessage)
            {
                case SocketBackend.Enumeration.TypeMessage.GAME_REPLAY:
                    EventService.Instance.RaiseShowControl();
                    break;
                case SocketBackend.Enumeration.TypeMessage.VALID_RULE:
                    GameManager.Instance.ReloadGame();
                    break;
                case SocketBackend.Enumeration.TypeMessage.MSG_CHAT:
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Messages.Add(e);
                    }));
                    break;
            }
        }

        public async Task SendMessage()
        {
            if(inputMessage != null)
            {
                await SocketService.Instance.Client.SendMessageAsync(new Message(UserSession.Instance.CurrentUser.Name ?? string.Empty, inputMessage, SocketBackend.Enumeration.TypeMessage.MSG_CHAT));
            }
        }

        public async Task SendMessage(Message msg)
        {
            if(msg != null)
            {
                await SocketService.Instance.Client.SendMessageAsync(msg);
            }
        }

        public ICommand SendCommand
        {
            get { return new RelayCommand(() => Task.Run(() => SendMessage())); }
        }

        public ICommand SendReplay
        {
            get 
            {
                return new RelayCommand(async () =>
                {
                    UserMessage msg = new UserMessage(UserSession.Instance.CurrentUser, SocketBackend.Enumeration.TypeMessage.GAME_REPLAY);
                    await SendMessage(msg);
                });
            }
        }
    }
}
