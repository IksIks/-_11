using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;
using ДЗ_11.Views.Windows;
using System.Windows;
using ДЗ_11.Data;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

namespace ДЗ_11.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Команда открытия окна для добавления клиента
        /// <summary>Команда вывода всех клиентов</summary>
        public ICommand AddNewUserCommand { get; }
        private void OnAddNewUserCommandExecuted(object parametr)
        {
            AddClient addClientWindow = new AddClient();
            addClientWindow.ShowDialog();
        }
        private bool CanAddNewUserCommandExecute(object parametr)
        {
            if (RuleChoiseViewModel.CanSeeOrChangeText)
                return true;
            return false;
        }
        #endregion

        #region Команда смены пользователя
        /// <summary>Команда смены пользователя</summary>
        public ICommand ChangeRuleCommand { get; }
        private void OnChangeRuleCommandExecuted(object parameter)
        {
            RuleChoise ruleChoiseWindow = new RuleChoise();
            ruleChoiseWindow.Show();
            Application.Current.Windows[0].Close();
        }
        private bool CanChangeRuleCommandExecute(object parametr) => true;
        #endregion

        #region Команда удаление клиента
        /// <summary>Команда удаление клиента</summary>
        public ICommand DeleteClientCommand { get; }
        private void OnDeleteClientCommandExecuted(object parametr)
        {
            Clients.Remove(parametr as Client);
        }
        private bool CanDeleteClientCommandExecute(object parametr) => parametr is Client && RuleChoiseViewModel.CanSeeOrChangeText ? true: false;

        #endregion

        private string clientChanges;
        public string ClientChanges
        {
            get => clientChanges;
            set => Set(ref clientChanges, value);
        }

        /// <summary>
        /// основная коллекция клиентов
        /// </summary>
        private ObservableCollection<Client> clients = HelpClass.Clients;
        public ObservableCollection<Client> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }

        /// <summary>
        /// коллекция для хранения изменений
        /// </summary>
        private List<string> changedPropertys = new List<string>();

        public List<string> ChangedPropertys
        {
            get => changedPropertys;
            set => Set(ref changedPropertys, value);
        }


        /// <summary>
        /// Автоматические клиенты для тестирования
        /// </summary>
        private void CreateClients()
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
        private void UpdateChanges(object sender, PropertyChangedEventArgs e)
        {
            string rule = RuleChoiseViewModel.CanSeeOrChangeText ? "Менеджер" : "Консультант";
            if (sender is Client tempClient)
            {
                ClientChanges = $"{rule} изменил в {tempClient.Id} поле {tempClient.ClientPropertyTranslater[e.PropertyName]}, " +
                                $"время {tempClient.DateClientChange = DateTime.Now}";
            }
            ChangedPropertys.Add(ClientChanges);
        }
        private void Clients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= UpdateChanges;
            if (e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += UpdateChanges;
            switch(e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    {
                        ClientChanges = $"Менеджер добавил клиента {((e.NewItems[0] as Client).Id).ToString()}";
                        break;
                    }

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    {
                        ClientChanges = $"Менеджер удалил клиента {(e.OldItems[0] as Client).Id.ToString()}";
                        break;
                    }
            }
        }

       
        public MainWindowViewModel()
        {
            ChangeRuleCommand = new RelayCommand(OnChangeRuleCommandExecuted, CanChangeRuleCommandExecute);
            AddNewUserCommand = new RelayCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);
            DeleteClientCommand = new RelayCommand(OnDeleteClientCommandExecuted, CanDeleteClientCommandExecute);
            Clients.CollectionChanged += Clients_CollectionChanged;
            CreateClients();
            
        }

       
    }
}
