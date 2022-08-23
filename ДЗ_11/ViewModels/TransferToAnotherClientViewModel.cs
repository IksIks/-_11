using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ДЗ_11.Data;
using ДЗ_11.Infrastructure.Commands;
using ДЗ_11.Models;
using ДЗ_11.Services;
using ДЗ_11.ViewModels.Base;

namespace ДЗ_11.ViewModels
{
    internal class TransferToAnotherClientViewModel : ViewModel
    {
        public Client AnotherClient { get; set; }
        private string visibility = "Hidden";
        private string visibilityAccountBalance = "Hidden";
        private string selectedAccount;
        private Cash currency;
        private string xmlBalance;
        private double transferAmount;
        private double accountBalance = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;
        private List<string> clientAccount = new List<string>
        {
            "Основной счет",
            "Депозитный счет"
        };
        public Client CurrentClient { get; set; } = HelpClass.TempClient;

        public ObservableCollection<Client> Clients { get; set; } = HelpClass.Clients;



        public double TransferAmount
        {
            get { return transferAmount; }
            set{ Set(ref transferAmount, value); }
        }

        public double AccountBalance
        {
            get { return accountBalance; }
            set { Set(ref accountBalance, value); }
        }
        public string SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                Set(ref selectedAccount, value);
                VisibilityAccountBalance = "Visible";
                if (selectedAccount == "Депозитный счет")
                {
                    AccountBalance = HelpClass.TempClient.DepositAccount.BalanceRUB_Account;
                    Visibility = "Hidden";
                    XmlBalance = "Баланс, руб";
                }
                else
                {
                    AccountBalance = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;
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
        public string VisibilityAccountBalance
        {
            get { return visibilityAccountBalance; }
            set { Set(ref visibilityAccountBalance, value); }
        }
        public List<string> ClientAccount
        {
            get { return HelpClass.TempClient.DepositAccount.DepositNotExist ? new List<string> { "Основной счет" } : clientAccount; }
            set { Set(ref clientAccount, value); }
        }

        public Cash Currency
        {
            get { return currency; }
            set
            {
                Set(ref currency, value);
                switch (currency)
                {
                    case Cash.RUB:
                        AccountBalance = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;
                        break;
                    case Cash.USD:
                        AccountBalance = HelpClass.TempClient.NonDepositAccount.BalanceUSD_Account;
                        break;
                    case Cash.EUR:
                        AccountBalance = HelpClass.TempClient.NonDepositAccount.BalanceEURO_Account;
                        break;
                }
            }
        }

        public string XmlBalance
        {
            get { return xmlBalance; }
            set { Set(ref xmlBalance, value); }
        }

        #region Команда перевода среств клиента другому клиенту
        public ICommand TransferAmountCommand { get; }

        private bool CanTransferAmountCommandExecute(object parametr)
        {
            if (TransferAmount > AccountBalance || TransferAmount <= 0 || AnotherClient == null || AnotherClient.Id == CurrentClient.Id || SelectedAccount == null) return false;
            return true;
        }
        private void OnTransferAmountCommandExecuted(object parametr)
        {
            if (SelectedAccount == "Основной счет")
            {
                switch (Currency)
                {
                    case Cash.RUB:
                        HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account -= TransferAmount;
                        AnotherClient.NonDepositAccount.BalanceRUB_Account += TransferAmount;
                        break;
                    case Cash.USD:
                        HelpClass.TempClient.NonDepositAccount.BalanceUSD_Account -= TransferAmount;
                        AnotherClient.NonDepositAccount.BalanceUSD_Account += TransferAmount;
                        break;
                    case Cash.EUR:
                        HelpClass.TempClient.NonDepositAccount.BalanceEURO_Account -= TransferAmount;
                        AnotherClient.NonDepositAccount.BalanceEURO_Account += TransferAmount;
                        break;
                }
                ChangingCustomerData(AnotherClient);
            }
            else
            {
                HelpClass.TempClient.DepositAccount.BalanceRUB_Account -= TransferAmount;
                AnotherClient.DepositAccount.BalanceRUB_Account += TransferAmount;
                ChangingCustomerData(AnotherClient);
            }
            Application.Current.Windows[1].Close();
        }
        #endregion

        private void ChangingCustomerData(Client client)
        {
            for (int i = 0; i < HelpClass.Clients.Count; i++)
            {
                if (HelpClass.Clients[i].Id == client.Id)
                    HelpClass.Clients[i] = client;
            }
        }


        public TransferToAnotherClientViewModel()
        {
            TransferAmountCommand = new RelayCommand(OnTransferAmountCommandExecuted, CanTransferAmountCommandExecute);
        }
    }
}
