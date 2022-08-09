using System.Windows;
using System.Windows.Input;
using ДЗ_11.Data;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class CashToAccountViewModel : ViewModel
    {
        private Cash currency;
        private int transferAmount;

        public int TransferAmount
        {
            get { return transferAmount; }
            set { Set (ref transferAmount, value); }
        }

        public Cash Currency
        {
            get { return currency; }
            set { Set (ref currency, value); }
        }
        #region Команды

        public ICommand CreditToAccountCommand { get; }
        private bool CanCreditToAccountCommandExecute(object parametr)
        {
            if (transferAmount == 0 ) return false;
            return true;
        }
        private void OnCreditToAccountCommandExecuted(object parametr)
        {
            HelpClass.TempClient.NonDepositAccount.Balance += TransferAmount;
            HelpClass.TempClient.NonDepositAccount.Currency = Currency;
            Application.Current.Windows[2].Close();

        }
        #endregion

        public CashToAccountViewModel()
        {
            CreditToAccountCommand = new RelayCommand(OnCreditToAccountCommandExecuted, CanCreditToAccountCommandExecute);
        }
    }
}
