using System.Collections.Generic;
using System.Windows;
using ДЗ_11.Data;
using ДЗ_11.Models.AbstractModels;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class TransferBetweenAccountsViewModel : ViewModel
    {
        private string balance;
        public string Balance
        {
            get { return balance; }
            set { Set(ref balance, value); }
        }

        private string selectedAccount;
        public string SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                Set(ref selectedAccount, value);
                if (selectedAccount == "Депозитный счет")
                {
                    Visibility = "Collapsed";
                    Balance = "Баланс, руб";
                }
                else
                {
                    Visibility = "Visible";
                    Balance = "Баланс";
                }
            }
        }
        private string visibility;
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
        public double TransferAmount { get; set; } = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;

        public Cash Currency { private get; set; }


        public TransferBetweenAccountsViewModel()
        {
           
           

        }
        
    }
}
