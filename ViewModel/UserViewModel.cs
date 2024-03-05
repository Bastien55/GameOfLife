using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOfLife.Service;
using SocketBackend;
using SocketBackend.Enumeration;
using SocketBackend.Messages;
using SocketBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameOfLife.ViewModel
{
    public class UserViewModel : ObservableObject
    {
        #region Properties
        public User Model => UserSession.Instance.CurrentUser;

        public string Name 
        {
            get { return Model.Name ?? string.Empty; }
            set 
            { 
                SetProperty(Model.Name, value, (name) => Model.Name = name);
                UpdateForDB();
            }
        }

        public string Rule
        {
            get => Model.Rule ?? string.Empty;
            set 
            {
                SetProperty(Model.Rule, value, (rule) => Model.Rule = rule);
                UpdateForDB();
            }
        }

        public bool IsLogged
        {
            get => UserSession.Instance.IsLoggedIn;
        }
        #endregion

        #region Methods
        public void Update()
        {
            OnPropertyChanged(nameof(IsLogged));
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(Rule));
        }

        public void UpdateForDB()
        {
            if (IsLogged)
            {
                Task.Run(async () => await SocketService.Instance.Client.SendMessageAsync(new UserMessage(Model, TypeMessage.USER_UPDATE)));
            }
        }
        #endregion

        #region ICommand
        public ICommand ConnectionCommand => new RelayCommand<TypeMessage>(async (param) =>
        {
            var msg = new UserMessage(Name,string.Empty, param, Rule);
            await SocketService.Instance.Client.SendMessageAsync(msg);
        });
        #endregion
    }
}
