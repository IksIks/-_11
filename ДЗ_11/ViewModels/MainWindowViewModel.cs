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
        private bool StatusBtnShowListUsers = true;

        #region Команда вывода всех клиентов
        /// <summary>Команда вывода всех клиентов</summary>
        public ICommand ShowListUsersCommand { get; }
        private void OnShowListUsersCommandExecuted(object parametr)
        {
            //CreateClients();
            StatusBtnShowListUsers = false;
        }
        private bool CanShowListUsersCommandExecute(object parametr)
        {
            if (StatusBtnShowListUsers) return true;
            return false;
        }

        #endregion

        public static ObservableCollection<Client> Clients = new ObservableCollection<Client>();

        #region Клиенты
        /// <summary>Список клиентов</summary>
        //private ObservableCollection<Client> clients;
        //public ObservableCollection<Client> Clients
        //{
        //    get { return clients; }
        //    set { Set(ref clients, value); }
        //} 
        #endregion

        //public void CreateClients()
        //{
        //    Random r = new Random();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Client temp = new Client("name" + i,
        //                                "LastName" + i,
        //                                "Patronymic" + i,
        //                                (uint)(i * Math.Pow(10, 8)),
        //                                new Passport((ushort)(i * Math.Pow(10, 32)),
        //                                (uint)(i * Math.Pow(10, 3))));
        //        Clients.Add(temp);
        //    }
        //}

        public MainWindowViewModel()
        {
            ShowListUsersCommand = new RelayCommand(OnShowListUsersCommandExecuted, CanShowListUsersCommandExecute);
            
        }

    }
}
