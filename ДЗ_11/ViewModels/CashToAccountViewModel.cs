using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ДЗ_11.Data;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class CashToAccountViewModel : ViewModel
    {
        public static event Action<string> NotifyAccountChange;
        public Client Client { get; } = HelpClass.TempClient;
        
        private double transferAmount;
        public double TransferAmount
        {
            get { return transferAmount; }
            set { Set (ref transferAmount, value); }
        }

        public Cash Currency { private get; set; }

        #region Команда пополнения счета клиента
        /// <summary>Команда пополнения счета клиента</summary>
        public ICommand CreditToAccountCommand { get; }
        private bool CanCreditToAccountCommandExecute(object parametr)
        {
            if (transferAmount != 0 ) return true;
            return false;
        }
        private void OnCreditToAccountCommandExecuted(object parametr)
        {
            switch (Currency)
            {
                case Cash.RUB: HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account += TransferAmount;
                    break;
                case Cash.USD: HelpClass.TempClient.NonDepositAccount.BalanceUSD_Account += TransferAmount;
                    break;
                case Cash.EUR: HelpClass.TempClient.NonDepositAccount.BalanceEURO_Account += TransferAmount;
                    break;
            }
            NotifyAccountChange?.Invoke($"{DateTime.Now} {Client.Id} {Client.LastName} {Client.Name} {Client.Patronymic}" +
                                        $" на {Currency} счет зачисленно {TransferAmount} {Currency}");
            Application.Current.Windows[1].Close();
        }
        #endregion

        public CashToAccountViewModel()
        {
            
            CreditToAccountCommand = new RelayCommand(OnCreditToAccountCommandExecuted, CanCreditToAccountCommandExecute);
        }
    }
}
