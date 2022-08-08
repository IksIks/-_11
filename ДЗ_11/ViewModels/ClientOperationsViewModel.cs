using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.ViewModels.Base;
using ДЗ_11.Views.Pages;
using ДЗ_11.Views.Windows;

namespace ДЗ_11.ViewModels
{
    internal class ClientOperationsViewModel : ViewModel
    {
        private Page moneyTransfers;
        private Page start;
       
        public Page MoneyTransfers
        {
            get { return moneyTransfers; }
            set { Set (ref moneyTransfers, value); }
        }
        
        public Page Start
        {
            get { return start; }
            set { Set(ref start, value); }
        }


        
        public ICommand OpenPageCommand { get; }
        private void OnOpenPageCommandExecuted(object parametr)
        {
            byte buttonIndexInXAML = Convert.ToByte(parametr);
            switch (buttonIndexInXAML)
            {
                case 1: Start = MoneyTransfers;
                    break;
                case 2: Start = MoneyTransfers;
                    break;
                case 3: Start = MoneyTransfers;
                    break;
                case 4: Start = MoneyTransfers;
                    break;
            }            
        }
        private bool CanOpenPageCommandExecute(object parametr) => true;

        public ClientOperationsViewModel()
        {
            OpenPageCommand = new RelayCommand(OnOpenPageCommandExecuted, CanOpenPageCommandExecute);
            MoneyTransfers = new Views.Pages.MoneyTransfer();
            
        }
    }
}
