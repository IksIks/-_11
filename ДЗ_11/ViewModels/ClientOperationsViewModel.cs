using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ДЗ_11.Data;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class ClientOperationsViewModel : ViewModel
    {
        private Page transferToAnotherClient;
        private Page currentPage;
        private Page cashToAccount;
        private Page transferBetweenAccounts;
        private Page createDepositAccount;
        private Page closeAccounts;
        bool blockButton;

        #region Страницы
        /// <summary>Страница закрытия счета</summary>
        public Page CloseAccounts
        {
            get { return closeAccounts; }
            set { Set(ref closeAccounts, value); }
        }

        /// <summary>Страница перевода между своими счетами</summary>
        public Page TransferBetweenAccounts
        {
            get { return transferBetweenAccounts; }
            set { Set(ref transferBetweenAccounts, value); }
        }

        /// <summary>Страница перевода между своими счетами</summary>
        public Page CashToAccount
        {
            get { return cashToAccount; }
            set { Set(ref cashToAccount, value); }
        }

        /// <summary>Страница переводу жругому клиенту</summary>
        public Page TransferToAnotherClient
        {
            get { return transferToAnotherClient; }
            set { Set(ref transferToAnotherClient, value); }
        }

        /// <summary>Страница для поля Frame в XAML</summary>
        public Page CurrentPage
        {
            get { return currentPage; }
            set { Set(ref currentPage, value); }
        }

        /// <summary>Страница создания Депозитного счета</summary>
        public Page CreateDepositAccount
        {
            get { return createDepositAccount; }
            set { Set(ref createDepositAccount, value); }
        }
        #endregion

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
                case 3: CurrentPage = TransferToAnotherClient;
                    break;
                case 4: CurrentPage = CloseAccounts;
                    {
                        MessageBox.Show("Будьте внимательны, отменить выбранные действия нельзя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        CurrentPage = CloseAccounts;
                        blockButton = true;
                    }
                    break;
            }
        }
        private bool CanOpenPageCommandExecute(object parametr)
        { 
            if(blockButton) return false;
            return true;
        }

        #endregion

        #region Команда открытия депозитного счета
        public ICommand OpenPageCreatDepositCommand { get; }
        private void OnOpenPageCreatDepositCommandExecuted(object parametr)
        {
            CurrentPage = CreateDepositAccount;
        }
        private bool CanOpenPageCreatDepositCommandExecute(object parametr)
        {
            if (HelpClass.TempClient.DepositAccount.DepositNotExist && !blockButton)
            { 
                return true;
            }
            return false;
        }

        #endregion

        public ClientOperationsViewModel()
        {
            OpenPageCommand = new RelayCommand(OnOpenPageCommandExecuted, CanOpenPageCommandExecute);
            OpenPageCreatDepositCommand = new RelayCommand(OnOpenPageCreatDepositCommandExecuted, CanOpenPageCreatDepositCommandExecute);
            TransferToAnotherClient = new Views.Pages.MoneyTransfer();
            TransferBetweenAccounts = new Views.Pages.TransferBetweenAccounts();
            CashToAccount = new Views.Pages.CashToAccount();
            CreateDepositAccount = new Views.Pages.DepositAccount();
            CloseAccounts = new Views.Pages.CloseAccounts();
        }
    }
}
