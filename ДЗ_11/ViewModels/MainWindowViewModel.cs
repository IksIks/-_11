using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ДЗ_11.Data;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;
using ДЗ_11.Views.Windows;


namespace ДЗ_11.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        
        public string ValuteCurse { get; private set; } = $"Курс валют на {DateTime.Now:dd/MM/yyyy}";
        public Tuple<string, string, double> ValuteEURCourse { get; private set; }
        public Tuple<string, string, double> ValuteUSDCourse { get; private set; }

        private string role = RoleChoiseViewModel.ManadgerRole ? "Менеджер" : "Консультант";
        private readonly string bankClients = "bankClients.txt";

        /// <summary> Коллекция для хранения изменений</summary>
        public List<string> ChangedPropertys { get; set; } = new List<string>();

        /// <summary> Основная коллекция клиентов</summary>
        private ObservableCollection<Client> clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get => clients;
            set => Set(ref clients, value);
        }

        #region Свойство для отображения изменений в окне
        private string clientChanges;
        public string ClientChanges
        {
            get => clientChanges;
            set => Set(ref clientChanges, value);
        }
        #endregion

        #region Команды управления

        #region Команда открытия окна для добавления клиента
        /// <summary>Команда открытия окна для добавления и его добавление клиента</summary>
        public ICommand AddNewUserCommand { get; }
        private void OnAddNewUserCommandExecuted(object parametr)
        {
            AddClient addClientWindow = new AddClient();
            addClientWindow.ShowDialog();
            if (String.IsNullOrEmpty(HelpClass.TempClient.Name))
                Clients.Remove(HelpClass.TempClient);
            else Clients.Add(HelpClass.TempClient);
        }
        private bool CanAddNewUserCommandExecute(object parametr)
        {
            if (RoleChoiseViewModel.ManadgerRole) return true;
            return false;
        }
        #endregion

        #region Команда смены пользователя
        /// <summary>Команда смены пользователя</summary>
        public ICommand ChangeRoleCommand { get; }
        private void OnChangeRoleCommandExecuted(object parameter)
        {
            RoleChoise roleChoiseWindow = new RoleChoise();
            roleChoiseWindow.Show();
            Application.Current.Windows[0].Close();
        }
        private bool CanChangeRoleCommandExecute(object parametr) => true;
        #endregion

        #region Команда удаление клиента
        /// <summary>Команда удаление клиента</summary>
        public ICommand DeleteClientCommand { get; }
        private void OnDeleteClientCommandExecuted(object parameter)
        {
            Clients.Remove(parameter as Client);
        }
        private bool CanDeleteClientCommandExecute(object parameter) => parameter is Client && RoleChoiseViewModel.ManadgerRole; /*? true : false;*/

        #endregion

        #region Команда сохранения клиентов и логов
        /// <summary>
        /// Команда сохранения клиентов и логов
        /// </summary>
        public ICommand SaveCommand { get; }
        private void OnSaveCommandExecuted(object parametr)
        {
            string fileLog = "bankLog.txt";
            if (!File.Exists(bankClients))
                File.Create(bankClients).Close();
            using (StreamWriter write = new StreamWriter(bankClients))
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
            ClientChanges = $"{role} сохранил клиентов в базу";
            ChangedPropertys.Add(clientChanges);
        }
        private bool CanSaveCommandExecute(object parametr)
        {
            if (Clients.Count > 0)
            {
                RoleChoiseViewModel.ManadgerRole = true;
                return true;
            }
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
            using (StreamReader read = new StreamReader(bankClients))
            {
                string line;
                while (!read.EndOfStream)
                {
                    line = read.ReadLine();
                    var splitLine = line.Split(' ');
                    Client client = new Client(Guid.Parse(splitLine[0]), splitLine[1], splitLine[2],
                                                          splitLine[3], splitLine[4], splitLine[5],
                                                          DateTime.Parse((splitLine[6]) + " " + (splitLine[7])),
                                                          new NonDepositAccount(DateTime.Parse((splitLine[10]) + " " + (splitLine[11])),
                                                                                double.Parse(splitLine[12]),
                                                                                double.Parse(splitLine[13]),
                                                                                double.Parse(splitLine[14]),
                                                                                DateTime.Parse((splitLine[15]) + " " + (splitLine[16]))),
                                                          new DepositAccount(DateTime.Parse((splitLine[19]) + " " + (splitLine[20])),
                                                                             double.Parse(splitLine[21]),
                                                                             double.Parse(splitLine[22]),
                                                                             DateTime.Parse((splitLine[23]) + " " + (splitLine[24])),
                                                                             Convert.ToBoolean(splitLine[25])));
                    Clients.Add(client);
                }
            }
            ClientChanges = $"{role} загрузил клиентов из базы";
            ChangedPropertys.Add(ClientChanges);
        }

        private bool CanRestoreBankClientsCommandExecute(object parameter)
        {
            if (!File.Exists(bankClients))
                return false;
            return (Clients.Count > 0) ? false : true;
        }
        #endregion

        #region Команда закрытия приложения
        public ICommand CloseAplicationCommand { get; }
        private void OnCloseAplicationCommandExecuted(object parameter)
        {
            Application.Current.Windows[0].Close();
        }
        private bool CanCloseAplicationCommandEcecute(object parameter) => true;
        #endregion

        #region Команда выполнения операции с клиентом
        public ICommand ClientOperationsCommand { get; }
        private void OnClientOperationsCommadExecuted(object parameter)
        {
            HelpClass.TempClient = parameter as Client;
            ClientOperations ClientOps = new ClientOperations();
            ClientOps.ShowDialog();
        }
        private bool CanClientOperationsCommandExecute(object parameter) => parameter is Client client && RoleChoiseViewModel.ManadgerRole;
        #endregion
        #endregion


        /// <summary>
        /// Автоматические клиенты для тестирования
        /// </summary>
        //private void CreateClients()
        //{
        //    for (int i = 1; i < 6; i++)
        //    {
        //        Client temp = new Client("LastName" + i,
        //                                "name" + i,
        //                                "Patronymic" + i,
        //                                (i * Math.Pow(10, 8)).ToString(),
        //                                (i * 1111111111).ToString());

        //       Clients.Add(temp);
        //    }
        //}

        /// <summary> Метод для отслеживания изменений в свойствах елементов коллекции Clients </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteChanges(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Client tempClient)
            {
                ClientChanges = $"{role} изменил в {tempClient.Id} поле {tempClient.ClientPropertyTranslater[e.PropertyName]}, " +
                                $"время {tempClient.DateClientChange = DateTime.Now}";
            }
            ChangedPropertys.Add(ClientChanges);
        }

        /// <summary> Событие для отслеживания изменений в коллекции Clients </summary>
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
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    {
                        ClientChanges = $"{role} добавил клиента {(e.NewItems[0] as Client).Id}";
                        ChangedPropertys.Add(ClientChanges);
                        break;
                    }

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    {
                        ClientChanges = $"Менеджер удалил клиента {(e.OldItems[0] as Client).Id}";
                        ChangedPropertys.Add(ClientChanges);
                        break;
                    }
            }
        }


        public MainWindowViewModel()
        {
            ChangeRoleCommand = new RelayCommand(OnChangeRoleCommandExecuted, CanChangeRoleCommandExecute);
            AddNewUserCommand = new RelayCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);
            DeleteClientCommand = new RelayCommand(OnDeleteClientCommandExecuted, CanDeleteClientCommandExecute);
            SaveCommand = new RelayCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            RestoreBankClientsCommand = new RelayCommand(OnRestoreBankClientsCommandExecuted, CanRestoreBankClientsCommandExecute);
            CloseAplicationCommand = new RelayCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandEcecute);
            ClientOperationsCommand = new RelayCommand(OnClientOperationsCommadExecuted, CanClientOperationsCommandExecute);
            Clients.CollectionChanged += Clients_CollectionChanged;
            //GetValute getValute = new GetValute();
            ValuteUSDCourse = GetValute.GetDataCurrentValute(Cash.USD);
            ValuteEURCourse = GetValute.GetDataCurrentValute(Cash.EUR);
            
        }
    }
}
