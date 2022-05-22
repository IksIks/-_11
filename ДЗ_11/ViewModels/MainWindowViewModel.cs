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
using System.IO;

namespace ДЗ_11.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string fileName = "bankClients.txt";
        #region Свойство для отображения изменений в окне
        private string clientChanges;
        public string ClientChanges
        {
            get => clientChanges;
            set => Set(ref clientChanges, value);
        } 
        #endregion

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
        private void OnDeleteClientCommandExecuted(object parameter)
        {
            Clients.Remove(parameter as Client);
        }
        private bool CanDeleteClientCommandExecute(object parameter) => parameter is Client && RuleChoiseViewModel.CanSeeOrChangeText ? true: false;

        #endregion

        #region Команда сохранения клиентов и логов
        /// <summary>
        /// Команда сохранения клиентов и логов
        /// </summary>
        public ICommand SaveCommand { get; }
        private void OnSaveCommandExecuted(object parametr)
        {
            string fileLog = "bankLog.txt";
            
            if (!File.Exists(fileName))
                File.Create(fileName);
            using (StreamWriter write = new StreamWriter(fileName))
            {
                foreach (var item in Clients)
                {
                    write.WriteLine(item.ToString());
                }
            }
            using (StreamWriter write = File.AppendText(fileLog))
            {
                foreach (var item in ChangedPropertys)
                {
                    write.WriteLine(item);
                }
            }

        }
        private bool CanSaveCommandExecute(object parametr)
        {
            if (Clients.Count > 0) return true;
            return false;
        }
        #endregion

        #region Команда загрузки клиентов из файла
        /// <summary>
        /// Команда загрузки клиентов из файла
        /// </summary>
        public ICommand RestoreBankClientsCommand { get; }
        private void OnRestoreBankClientsCommandExecuted(object parameter)
        {
            using (StreamReader read = new StreamReader(fileName))
            {
                string line;
                while (!read.EndOfStream)
                {
                    line = read.ReadLine();
                    var splitLine = line.Split(' ');

                    Client client = new Client(Guid.Parse(splitLine[0]), splitLine[1], splitLine[2], splitLine[3], splitLine[4], splitLine[5], DateTime.Parse(splitLine[6]));

                    Clients.Add(client);
                }
            }
        }

        private bool CanRestoreBankClientsCommandExecute(object parameter) => (Clients.Count > 0) ? false : true; 
        #endregion

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
        //private List<string> changedPropertys = new List<string>();

        public List<string> ChangedPropertys { get; set; } = new List<string>();
        //{
        //    get => changedPropertys;
        //    set => Set(ref changedPropertys, value);
        //}

        /// <summary>
        /// Автоматические клиенты для тестирования
        /// </summary>
        private void CreateClients()
        {
            for (int i = 1; i < 6; i++)
            {
                Client temp = new Client("LastName" + i,
                                        "name" + i,
                                        "Patronymic" + i,
                                        (i * Math.Pow(10, 8)).ToString(),
                                        (i * 1111111111).ToString());
                                        
               Clients.Add(temp);
            }
        }

        /// <summary>
        /// Метод для отслеживания изменений в свойствах елементов коллекции Clients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteChanges(object sender, PropertyChangedEventArgs e)
        {
            string rule = RuleChoiseViewModel.CanSeeOrChangeText ? "Менеджер" : "Консультант";
            if (sender is Client tempClient)
            {
                ClientChanges = $"{rule} изменил в {tempClient.Id} поле {tempClient.ClientPropertyTranslater[e.PropertyName]}, " +
                                $"время {tempClient.DateClientChange = DateTime.Now}";
            }
            ChangedPropertys.Add(ClientChanges);
        }

        /// <summary>
        /// Событие для отслеживания изменений в коллекции Clients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= WriteChanges;
            if (e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += WriteChanges;
            switch(e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    {
                        ClientChanges = $"Менеджер добавил клиента {((e.NewItems[0] as Client).Id).ToString()}";
                        ChangedPropertys.Add(ClientChanges);
                        break;
                    }

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    {
                        ClientChanges = $"Менеджер удалил клиента {(e.OldItems[0] as Client).Id.ToString()}";
                        ChangedPropertys.Add(ClientChanges);
                        break;
                    }
            }
        }

       
        public MainWindowViewModel()
        {
            ChangeRuleCommand = new RelayCommand(OnChangeRuleCommandExecuted, CanChangeRuleCommandExecute);
            AddNewUserCommand = new RelayCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);
            DeleteClientCommand = new RelayCommand(OnDeleteClientCommandExecuted, CanDeleteClientCommandExecute);
            SaveCommand = new RelayCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            RestoreBankClientsCommand = new RelayCommand(OnRestoreBankClientsCommandExecuted, CanRestoreBankClientsCommandExecute);
            Clients.CollectionChanged += Clients_CollectionChanged;
            //CreateClients();
            
        }

       
    }
}
