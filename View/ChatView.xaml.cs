using GameOfLife.Events;
using GameOfLife.Service;
using SocketBackend;
using SocketBackend.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLife.View
{
    /// <summary>
    /// Logique d'interaction pour ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
            EventService.Instance.OnShowControl += Handler_OnShowControl;
            EventService.Instance.OnErrorLogin += Handler_OnErrorLogin; ;
        }

        private void Handler_OnErrorLogin(object? sender, UserMessage e)
        {
            MessageBox.Show($"Pour l'utilisateur {e.Name} : {e.ContentMessage}");
        }

        private async void Handler_OnShowControl(object? sender, UserMessage e)
        {
            var result = MessageBox.Show($"L'utilisateur {e.Name} propose une règle {e.Rule} voulez-vous l'acceptez ?", "Message", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                UserSession.Instance.CurrentRule = new Models.Rule(e.Rule);
                await SocketService.Instance.Client.SendMessageAsync(new Message(SocketBackend.Enumeration.TypeMessage.VALID_RULE));
            }
            else
                await SocketService.Instance.Client.SendMessageAsync(new Message(SocketBackend.Enumeration.TypeMessage.INVALID_RULE));
        }
    }
}
