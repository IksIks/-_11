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
        private Page currentPage;
        private Page cashToAccount;

        public Page CashToAccount
        {
            get { return cashToAccount; }
            set { Set(ref cashToAccount, value); }
        }

        public Page MoneyTransfers
        {
            get { return moneyTransfers; }
            set { Set(ref moneyTransfers, value); }
        }
        
        public Page CurrentPage
        {
            get { return currentPage; }
            set { Set(ref currentPage, value); }
        }


        
        public ICommand OpenPageCommand { get; }
        private void OnOpenPageCommandExecuted(object parametr)
        {
            byte buttonIndexInXAML = Convert.ToByte(parametr);
            switch (buttonIndexInXAML)
            {
                case 1: CurrentPage = CashToAccount;
                    break;
                case 2: CurrentPage = MoneyTransfers;
                    break;
                case 3: CurrentPage = MoneyTransfers;
                    break;
                case 4: CurrentPage = MoneyTransfers;
                    break;
            }            
        }
        private bool CanOpenPageCommandExecute(object parametr) => true;

        public ClientOperationsViewModel()
        {
            OpenPageCommand = new RelayCommand(OnOpenPageCommandExecuted, CanOpenPageCommandExecute);
            MoneyTransfers = new Views.Pages.MoneyTransfer();
            CashToAccount = new Views.Pages.CashToAccount();
        }
    }
}
