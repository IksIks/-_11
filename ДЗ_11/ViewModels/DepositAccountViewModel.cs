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
        private double transferAmount; 
        public double TransferAmount
        {
            get { return transferAmount; }
            set { Set( ref transferAmount, value); }
        }
        
        private double depositPercent = HelpClass.TempClient.DepositAccount.DepositPercent;
        public double DepositPercent
        {
            get { return depositPercent; }
            set { Set(ref depositPercent, value); }
        }

        public ICommand CreditToAccountCommand { get; }
        private bool CanCreditToAccountCommandExecute(object parametr)
        {
            if (transferAmount != 0) return true;
            return false;
        }
        private void OnCreditToAccountCommandExecuted(object parametr)
        {
            HelpClass.TempClient.DepositAccount.BalanceRUB_Account = TransferAmount;
            HelpClass.TempClient.DepositAccount.DateOfCreation = DateTime.Now;
            HelpClass.TempClient.DepositAccount.DepositPercent = DepositPercent;
            HelpClass.TempClient.DepositAccount.DepositNotExist = false;
            Application.Current.Windows[1].Close();
        }
        public Client Client { get; private set; } = HelpClass.TempClient;

        public DepositAccountViewModel()
        {
            CreditToAccountCommand = new RelayCommand(OnCreditToAccountCommandExecuted, CanCreditToAccountCommandExecute);
        }

    }
}
