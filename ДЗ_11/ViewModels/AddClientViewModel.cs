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
        public Client NewClient { get; set; } = new Client();

        //public Client NewClient
        //{
        //    get { return newClient; }
        //    set { Set(ref newClient, value); }
        //}

        public ICommand CreateNewClientCommand { get; }
        private void OnCreateNewClientCommandExecuted(object parameter)
        {
            HelpClass.Clients.Add(NewClient);
            Application.Current.Windows[1].Close();
        }
        private bool CanCreateNewClientCommandExecute(object parameter) => true;




        public AddClientViewModel()
        {
            CreateNewClientCommand = new RelayCommand(OnCreateNewClientCommandExecuted, CanCreateNewClientCommandExecute);
            
        }
    }
}
