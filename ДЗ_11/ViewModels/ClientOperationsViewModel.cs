using System;
using System.Windows.Controls;
using System.Windows.Input;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class ClientOperationsViewModel : ViewModel
    {
        private Page moneyTransfers;
        private Page currentPage;
        private Page cashToAccount;
        private Page transferBetweenAccounts;
        private Page createDepositAccount;

        public Page TransferBetweenAccounts
        {
            get { return transferBetweenAccounts; }
            set { Set(ref transferBetweenAccounts, value); }
        }

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

        public Page CreateDepositAccount
        {
            get { return createDepositAccount; }
            set { Set(ref createDepositAccount, value); }
        }

        #region Команда открытия страницы
        public ICommand OpenPageCommand { get; }
        private void OnOpenPageCommandExecuted(object parametr)
        {
            byte buttonIndexInXAML = Convert.ToByte(parametr);
            switch (buttonIndexInXAML)
            {
                case 1: CurrentPage = CashToAccount;
                    break;
                case 2: CurrentPage = TransferBetweenAccounts;
                    break;
                case 3: CurrentPage = MoneyTransfers;
                    break;
                case 4: CurrentPage = CreateDepositAccount;
                    break;
            }
        }         
        private bool CanOpenPageCommandExecute(object parametr) => true;
        #endregion


        public ClientOperationsViewModel()
        {
            OpenPageCommand = new RelayCommand(OnOpenPageCommandExecuted, CanOpenPageCommandExecute);
            MoneyTransfers = new Views.Pages.MoneyTransfer();
            TransferBetweenAccounts = new Views.Pages.TransferBetweenAccounts();
            CashToAccount = new Views.Pages.CashToAccount();
            CreateDepositAccount = new Views.Pages.DepositAccount();
        }
    }
}
