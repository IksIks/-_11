using System;
using System.Windows;
using System.Windows.Input;
using ДЗ_11.Data;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class DepositAccountViewModel : ViewModel
    {
        public static event Action<string> OpenDepositAccount;
        public double TransferAmount
        {
            get { return transferAmount; }
            set { Set(ref transferAmount, value); }
        }
        public Client Client { get; private set; } = HelpClass.TempClient;
        public double DepositPercent
        {
            get { return depositPercent; }
            set { Set(ref depositPercent, value); }
        }
        public DepositAccountViewModel()
        {
            CreditToAccountCommand = new RelayCommand(OnCreditToAccountCommandExecuted, CanCreditToAccountCommandExecute);
        }

        private double transferAmount;
        private double depositPercent = HelpClass.TempClient.DepositAccount.DepositPercent;

        public ICommand CreditToAccountCommand { get; }
        private bool CanCreditToAccountCommandExecute(object parametr)
        {
            if (transferAmount != 0) return true;
            return false;
        }
        private void OnCreditToAccountCommandExecuted(object parametr)
        {
            Client.DepositAccount.BalanceRUB_Account = TransferAmount;
            Client.DepositAccount.DateOfCreation = DateTime.Now;
            Client.DepositAccount.DepositPercent = DepositPercent;
            Client.DepositAccount.DepositNotExist = false;
            OpenDepositAccount?.Invoke($"{Client.DepositAccount.DateOfCreation} {Client.Id} {Client.LastName} {Client.Name} {Client.Patronymic} Открыт депозитный счет");
            Application.Current.Windows[1].Close();
        }


    }
}
