using System;
using System.Windows.Input;
using ДЗ_11.Data;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;

namespace ДЗ_11.ViewModels
{
    internal class CloseAccountsViewModel
    {
        public Client Client { get; } = HelpClass.TempClient;
        public static event Action<string> CloseAccount;

        #region Команда закрытия основного счета
        /// <summary>Команда закрытия основного счета</summary>
        public ICommand CloseNonDepositeAccountCommand { get; }
        private bool CanCloseNonDepositeAccountCommandExecute(object parameter)
        {
            if (Client.DepositAccount.DepositNotExist || Client.NonDepositAccount.BalanceRUB_Account == 0
                                                         && Client.NonDepositAccount.BalanceUSD_Account == 0
                                                         && Client.NonDepositAccount.BalanceEURO_Account == 0) return false;
            return true;
        }
        private void OnCloseNonDepositeAccountCommandExecuted(object parameter)
        {
            Client.DepositAccount.BalanceRUB_Account += Client.NonDepositAccount.BalanceRUB_Account;
            Client.NonDepositAccount.BalanceRUB_Account = 0;
            Client.DepositAccount.BalanceRUB_Account += Client.NonDepositAccount.BalanceUSD_Account * GetValute.GetDataCurrentValute(Services.Cash.USD).Item3;
            Client.NonDepositAccount.BalanceUSD_Account = 0;
            Client.DepositAccount.BalanceRUB_Account += Client.NonDepositAccount.BalanceEURO_Account * GetValute.GetDataCurrentValute(Services.Cash.EUR).Item3;
            Client.NonDepositAccount.BalanceEURO_Account = 0;
            CloseAccount?.Invoke($"{DateTime.Now} {Client.Id} {Client.LastName} {Client.Name} {Client.Patronymic} Основной счет закрыт");
        }
        #endregion

        #region Команда закрытия депозитного счета
        /// <summary>Команда закрытия депозитного счета</summary>
        public ICommand CloseDepositeAccountCommand { get; }
        private bool CanCloseDepositeAccountCommandExecute(object parameter)
        {
            if (Client.DepositAccount.DepositNotExist || Client.NonDepositAccount.BalanceRUB_Account == 0
                                                         && Client.NonDepositAccount.BalanceUSD_Account == 0
                                                         && Client.NonDepositAccount.BalanceEURO_Account == 0) return false;
            return true;
        }
        private void OnCloseDepositeAccountCommandExecuted(object parameter)
        {
            Client.NonDepositAccount.BalanceRUB_Account += Client.DepositAccount.BalanceRUB_Account;
            Client.DepositAccount.BalanceRUB_Account = 0;
            Client.DepositAccount.DateOfClose = DateTime.Now;
            Client.DepositAccount.DepositNotExist = true;
            CloseAccount?.Invoke($"{Client.DepositAccount.DateOfClose} {Client.Id} {Client.LastName} {Client.Name} {Client.Patronymic} Депозитный счет закрыт");
        } 
        #endregion

        public CloseAccountsViewModel()
        {
            CloseDepositeAccountCommand = new RelayCommand(OnCloseDepositeAccountCommandExecuted, CanCloseDepositeAccountCommandExecute);
            CloseNonDepositeAccountCommand = new RelayCommand(OnCloseNonDepositeAccountCommandExecuted, CanCloseNonDepositeAccountCommandExecute);
        }
    }
}
