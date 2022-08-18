using System.Collections.Generic;
using ДЗ_11.Data;
using ДЗ_11.Models;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class TransferBetweenAccountsViewModel : ViewModel
    {
        public Client Client { get; set; } = HelpClass.TempClient;
        private string debitAccount;
        private double transferAmount;
        private string xmlBalance;
        private string selectedAccount;
        private string visibility;
        private double accountBalance = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;

        public string DebitAccount
        {
            get { return debitAccount; }
            set { Set( ref debitAccount, value); }
        }
        public double TransferAmount
        {
            get { return transferAmount; }
            set { Set( ref transferAmount, value); }
        }

        public string XmlBalance
        {
            get { return xmlBalance; }
            set { Set(ref xmlBalance, value); }
        }

        public string SelectedAccount
        {
            get
            {
                if (string.IsNullOrEmpty(selectedAccount))
                    Visibility = "Visible";
                return selectedAccount;
            }
            set
            {
                Set(ref selectedAccount, value);
                if (selectedAccount == "Депозитный счет")
                {
                    DebitAccount = "Основной счет";
                    Visibility = "Collapsed";
                    XmlBalance = "Баланс, руб";
                }
                else
                {
                    DebitAccount = "Депозитный счет";
                    Visibility = "Visible";
                    XmlBalance = "Баланс";
                }
            }
        }
        public string Visibility
        {
            get { return visibility; }
            set { Set(ref visibility, value); }

        }

        private List<string> clientAccount = new List<string>
        {
            "Основной счет",
            "Депозитный счет"
        };
        public List<string> ClientAccount
        {
            get { return clientAccount; }
            set { Set(ref clientAccount, value); }
        }

        
        public double AccountBalance
        {
            get { return accountBalance; }
            set { Set(ref accountBalance, value); }
            
        }
        private Cash currency;
        public Cash Currency
        {
            get { return currency; }
            set
            {
                Set(ref currency, value);
                switch (currency)
                {
                    case Cash.RUB: AccountBalance = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;
                        break;
                    case Cash.USD:  AccountBalance = HelpClass.TempClient.NonDepositAccount.BalanceUSD_Account;
                        break;
                    case Cash.EURO: AccountBalance = HelpClass.TempClient.NonDepositAccount.BalanceEURO_Account;
                        break;
                        default: AccountBalance = HelpClass.TempClient.DepositAccount.BalanceRUB_Account;
                        break;
                }
            }
        }

        public TransferBetweenAccountsViewModel()
        {



        }

    }
}
