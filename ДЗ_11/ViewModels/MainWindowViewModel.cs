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
        public ICommand ShowListUsersCommand { get; }

        private void OnShowListUsersCommandExecuted(object parametr)
        {
            AddClients();
        }

        private bool CanShowListUsersCommandExecute(object parametr) => true;



        private ObservableCollection<Client> users = new ObservableCollection<Client>();
        public ObservableCollection<Client> Users
        {
            get { return users; }
            set { Set(ref users, value); }
        }

        public void AddClients()
        {
            Random r = new Random();
            for (int i = 0; i < 15; i++)
            {
                Client temp = new Client("name" + i, "LastName" + i, "Patroinymic" + i, (uint)(i * Math.Pow(10, 8))
                                        , new Passport((ushort)(i * Math.Pow(10, 32)), (uint)(i * Math.Pow(10, 3))));
                Users.Add(temp);
            }
            //Test = Users;
        }

        public MainWindowViewModel()
        {
            //    for (int i = 0; i < 15; i++)
            //    {
            //        Client temp = new Client("name" + i, "LastName" + i, "Patroinymic" + i, (uint)(i * Math.Pow(10, 8))
            //                                , new Passport((ushort)(i * Math.Pow(10, 32)), (uint)(i * Math.Pow(10, 3))));
            //        users.Add(temp);
            //    }
            //    Test = users;

            ShowListUsersCommand = new RelayCommand(OnShowListUsersCommandExecuted, CanShowListUsersCommandExecute);
        }

    }
}
