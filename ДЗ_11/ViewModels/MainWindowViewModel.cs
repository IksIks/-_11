using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public bool BtnShowListUsersStatus = true;
        #region Команда вывода всех пользователей
        public ICommand ShowListUsersCommand { get; }

        private void OnShowListUsersCommandExecuted(object parametr)
        {
            AddClients();
            BtnShowListUsersStatus = false;
        }
        private bool CanShowListUsersCommandExecute(object parametr)
        {
            if (BtnShowListUsersStatus) return true;
            return false;
        }
       
        #endregion

        private ObservableCollection<Client> users = new ObservableCollection<Client>();
        public ObservableCollection<Client> Users
        {
            get { return users; }
            set { Set(ref users, value); }
        }

        public void AddClients()
        {
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                Client temp = new Client("name" + i, "LastName" + i, "Patroinymic" + i, (uint)(i * Math.Pow(10, 8))
                                        , new Passport((ushort)(i * Math.Pow(10, 32)), (uint)(i * Math.Pow(10, 3))));
                Users.Add(temp);
            }
        }

        public MainWindowViewModel()
        {
            ShowListUsersCommand = new RelayCommand(OnShowListUsersCommandExecuted, CanShowListUsersCommandExecute);
        }

    }
}
