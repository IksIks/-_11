using System;
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
        /// <summary>Сумма для зачисления на счет</summary>
        public double TransferAmount
        {
            get { return transferAmount; }
            set { Set(ref transferAmount, value); }
        }
        /// <summary>Количество средств на выбранном счете</summary>
        public double AccountBalance
        {
            get { return accountBalance; }
            set { Set(ref accountBalance, value); }
        }

        /// <summary>Выбранный пользователем счет в разметке</summary>
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

        /// <summary>Свойство видимости элементов разметки в зависимости от выбранного счета</summary>
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

        /// <summary>Текстовое представление клиентских аккаунтов</summary>
        public List<string> ClientAccount
        {
            get { return HelpClass.TempClient.DepositAccount.DepositNotExist ? new List<string> { "Основной счет" } : clientAccount; }
            set { Set(ref clientAccount, value); }
        }

        /// <summary>Подсчета "Основного счета" клиента</summary>
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

        /// <summary>Текстовое поле для указания счета в разметке</summary>
        public string XmlBalance
        {
            get { return xmlBalance; }
            set { Set(ref xmlBalance, value); }
        }
        public Client Client { get; private set; } = HelpClass.TempClient;
        public ObservableCollection<Client> Clients { get; set; } = HelpClass.Clients;
        public static event Action<string> NotifyTransferToAnotherClient;

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

        public TransferToAnotherClientViewModel()
        {
            TransferAmountCommand = new RelayCommand(OnTransferAmountCommandExecuted, CanTransferAmountCommandExecute);
        }


        #region Команда перевода среств клиента другому клиенту
        public ICommand TransferAmountCommand { get; }

        private bool CanTransferAmountCommandExecute(object parametr)
        {
            if (TransferAmount > AccountBalance || TransferAmount <= 0 || AnotherClient == null || AnotherClient.Id == Client.Id || SelectedAccount == null) return false;
            return true;
        }
        private void OnTransferAmountCommandExecuted(object parametr)
        {
            if (SelectedAccount == "Основной счет")
            {
                switch (Currency)
                {
                    case Cash.RUB:
                        Client.NonDepositAccount.BalanceRUB_Account -= TransferAmount;
                        AnotherClient.NonDepositAccount.BalanceRUB_Account += TransferAmount;
                        break;
                    case Cash.USD:
                        Client.NonDepositAccount.BalanceUSD_Account -= TransferAmount;
                        AnotherClient.NonDepositAccount.BalanceUSD_Account += TransferAmount;
                        break;
                    case Cash.EUR:
                        Client.NonDepositAccount.BalanceEURO_Account -= TransferAmount;
                        AnotherClient.NonDepositAccount.BalanceEURO_Account += TransferAmount;
                        break;
                }
                NotifyTransferToAnotherClient?.Invoke($"{DateTime.Now} {Client.Id} {Client.LastName} {Client.Name} {Client.Patronymic} " +
                                                          $"перевел {AnotherClient.Id} {AnotherClient.LastName} {AnotherClient.Name} {AnotherClient.Patronymic} " +
                                                          $"{TransferAmount} {Currency}");
                ChangingCustomerData(AnotherClient);
            }
            else
            {
                Client.DepositAccount.BalanceRUB_Account -= TransferAmount;
                AnotherClient.DepositAccount.BalanceRUB_Account += TransferAmount;
                NotifyTransferToAnotherClient?.Invoke($"{DateTime.Now} {Client.Id} {Client.LastName} {Client.Name} {Client.Patronymic} " +
                                                              $"перевел {AnotherClient.Id} {AnotherClient.LastName} {AnotherClient.Name} {AnotherClient.Patronymic} " +
                                                              $"{TransferAmount} {Currency}");
                ChangingCustomerData(AnotherClient);
            }
            Application.Current.Windows[1].Close();
        }
        #endregion

        /// <summary>Передача в основной список клиентов окна MainWindowViewModel</summary>
        /// <param name="client"></param>
        private void ChangingCustomerData(Client client)
        {
            for (int i = 0; i < HelpClass.Clients.Count; i++)
            {
                if (HelpClass.Clients[i].Id == client.Id)
                    HelpClass.Clients[i] = client;
            }
        }
    }
}
