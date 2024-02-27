using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOfLife.Service;
using SocketBackend;
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
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Messages.Add(e);
            }));
        }

        public async Task SendMessage()
        {
            if(inputMessage != null)
            {
                await SocketService.Instance.Client.SendMessageAsync("Guest : " + inputMessage);
                //Messages.Add(InputMessage);
            }
        }

        public ICommand SendCommand
        {
            get { return new RelayCommand(() => Task.Run(() => SendMessage())); }
        }
    }
}
