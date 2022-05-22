using System.Windows;
using System.Windows.Input;
using ДЗ_11.Data;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class AddClientViewModel : ViewModel
    {
        private Client newClient;
        public Client NewClient
        {
            get { return newClient; }
            set { Set(ref newClient, value); }
        }

        #region Команда добавления нового клиента
        /// <summary>Команда добавления нового клиента</summary>
        public ICommand CreateNewClientCommand { get; }
        private void OnCreateNewClientCommandExecuted(object parameter)
        {
            HelpClass.Clients.Add(NewClient);
            Application.Current.Windows[2].Close();
        }
        private bool CanCreateNewClientCommandExecute(object parameter) => true; 
        #endregion

        public AddClientViewModel()
        {
            CreateNewClientCommand = new RelayCommand(OnCreateNewClientCommandExecuted, CanCreateNewClientCommandExecute);
            NewClient = new Client();
        }
    }
}
