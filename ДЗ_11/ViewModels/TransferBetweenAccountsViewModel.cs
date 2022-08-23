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
    internal class TransferBetweenAccountsViewModel : ViewModel
    {
        public Client Client { get; set; } = HelpClass.TempClient;
        private string debitAccount;
        private double transferAmount;
        private string xmlBalance;
        private string selectedAccount;
        private string visibility = "Hidden";
        private string visibilityAccountBalance = "Hidden";
        private double accountBalance = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;
        private string сonversion;
        private double сonversionValute;


        public double ConversionValute
        {
            get { return сonversionValute; }
            set { Set(ref сonversionValute, value); }
        }

        public string Conversion
        {
            get { return сonversion = $"Сумма перевода {ConversionValute}"; }
            set { Set(ref сonversion, value); }
        }



        public double CostOneEuro { get; set; }
        public double CostOneDollar { get; set; }

        public string DebitAccount
        {
            get { return debitAccount; }
            set { Set(ref debitAccount, value); }
        }
        public double TransferAmount
        {
            get { return transferAmount; }
            set
            {
                Set(ref transferAmount, value);
                switch (Currency)
                {
                    case Cash.RUB: ConversionValute = TransferAmount;
                        break;
                    case Cash.USD:
                        {
                            ConversionValute = TransferAmount * GetValute.GetDataCurrentValute(Currency).Item3;

                        }
                        break;
                    case Cash.EUR: ConversionValute = TransferAmount * GetValute.GetDataCurrentValute(Currency).Item3;
                        break;
                    default:
                        break;
                }
            }
        }

        public string XmlBalance
        {
            get { return xmlBalance; }
            set { Set(ref xmlBalance, value); }
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
                    DebitAccount = "Основной счет";
                    Visibility = "Hidden";
                    XmlBalance = "Баланс, руб";
                }
                else
                {
                    AccountBalance = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;
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

        public string VisibilityAccountBalance
        {
            get { return visibilityAccountBalance; }
            set { Set(ref visibilityAccountBalance, value); }
        }

        private List<string> clientAccount = new List<string>
        {
            "Основной счет",
            "Депозитный счет"
        };
        public List<string> ClientAccount
        {
            get
            {
                return clientAccount = !Client.DepositAccount.DepositNotExist ? clientAccount : new List<string> { "Основной счет" };
            }
            set { Set(ref clientAccount, value); }
        }


        public double AccountBalance
        {
            get { return accountBalance; }
            set
            {

                Set(ref accountBalance, value);
            }
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

        public ICommand TransferAmountCommand { get; }

        private bool CanTransferAmountCommandExecute(object parametr)
        {
            if (TransferAmount > AccountBalance || TransferAmount <= 0) return false;
            return true;
        }
        private void OnTransferAmountCommandExecuted(object parametr)
        {
            var test = parametr as string;
            if(SelectedAccount == "Основной счет")
            {
                switch (Currency)
                {
                    case Cash.RUB:
                        HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account -= TransferAmount;
                        break;
                    case Cash.USD:
                        HelpClass.TempClient.NonDepositAccount.BalanceUSD_Account -= TransferAmount;
                        break;
                    case Cash.EUR:
                        HelpClass.TempClient.NonDepositAccount.BalanceEURO_Account -= TransferAmount;
                        break;
                }
                HelpClass.TempClient.DepositAccount.BalanceRUB_Account += ConversionValute;
            }
            else
            {
                HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account += ConversionValute;
                HelpClass.TempClient.DepositAccount.BalanceRUB_Account -= TransferAmount;
            }
            Application.Current.Windows[1].Close();
        }


        //private double CanValuteConversation(Cash cash)
        //{
        //    if(TransferAmount > AccountBalance || TransferAmount <= 0)
        //}
        public TransferBetweenAccountsViewModel()
        {
            TransferAmountCommand = new RelayCommand(OnTransferAmountCommandExecuted, CanTransferAmountCommandExecute);
        }

    }
}
