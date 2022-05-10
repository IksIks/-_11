using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;
using ДЗ_11.Views.Windows;
using System.Windows;

namespace ДЗ_11.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        //private bool StatusBtnShowListUsers = true;

        //private bool isReadOnly;
        public bool IsReadOnly { get; set; }
        

        #region Команда вывода всех клиентов
        /// <summary>Команда вывода всех клиентов</summary>
        public ICommand AddNewUserCommand { get; }
        private void OnAddNewUserCommandExecuted(object parametr)
        {
            AddClient addClientWindow = new AddClient();
            addClientWindow.ShowDialog();
            
        }
        private bool CanAddNewUserCommandExecute(object parametr) => true;

        #endregion
        private ObservableCollection<Client> clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }

        public void CreateClients()
        {
            Random r = new Random();
            for (int i = 1; i < 6; i++)
            {
                Client temp = new Client("name" + i,
                                        "LastName" + i,
                                        "Patronymic" + i,
                                        (uint)(i * Math.Pow(10, 8)),
                                        new Passport((i * 1111).ToString(),
                                        (i * 111111).ToString()));
                Clients.Add(temp);
            }
        }

        public MainWindowViewModel()
        {
            AddNewUserCommand = new RelayCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);
            CreateClients();
            IsReadOnly = !RuleChoiseViewModel.CanSeeText;

        }

    }
}
