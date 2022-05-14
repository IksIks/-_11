using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;
using ДЗ_11.Views.Windows;
using System.Windows;
using ДЗ_11.Data;

namespace ДЗ_11.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        //private bool StatusBtnShowListUsers = true;

       
        #region Команда открытия окна для добавления клиента
        /// <summary>Команда вывода всех клиентов</summary>
        public ICommand AddNewUserCommand { get; }
        private void OnAddNewUserCommandExecuted(object parametr)
        {
            AddClient addClientWindow = new AddClient();
            addClientWindow.ShowDialog();
        }
        private bool CanAddNewUserCommandExecute(object parametr) => true;
        #endregion

        #region Команда смены пользователя
        public ICommand ChangeRuleCommand { get; }
        private void OnChangeRuleCommandExecuted(object parameter)
        {
            RuleChoise ruleChoiseWindow = new RuleChoise();
            ruleChoiseWindow.Show();
            Application.Current.Windows[0].Close();
        }
        private bool CanChangeRuleCommandExecute(object parametr) => true; 
        #endregion


        private ObservableCollection<Client> clients = HelpClass.Clients;
        public ObservableCollection<Client> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }

        public void CreateClients()
        {
            for (int i = 1; i < 6; i++)
            {
                Client temp = new Client("name" + i,
                                        "LastName" + i,
                                        "Patronymic" + i,
                                        (i * Math.Pow(10, 8)).ToString(),
                                        (i * 1111111111).ToString());
               Clients.Add(temp);
            }
        }

        public MainWindowViewModel()
        {
            ChangeRuleCommand = new RelayCommand(OnChangeRuleCommandExecuted, CanChangeRuleCommandExecute);
            AddNewUserCommand = new RelayCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);
            CreateClients();
            
        }

    }
}
