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
    internal class TransferBetweenAccountsViewModel : ViewModel
    {
        private string anotherAccount;
        private double transferAmount;
        private string xmlBalance;
        private string selectedAccount;
        private string visibility = "Hidden"; // для работы с разметкой изменить на Visible
        private string visibilityAccountBalance = "Hidden"; // для работы с разметкой изменить на Visible
        private double accountBalance = HelpClass.TempClient.NonDepositAccount.BalanceRUB_Account;
        private double сonversionValute;
        private List<string> clientAccount = new List<string>
        {
            "Основной счет",
            "Депозитный счет"
        };
        private Cash currency;
        public static event Action<string> NotifyTransferBetweenAccounts;
        public Client Client { get; private set; } = HelpClass.TempClient;

        /// <summary>Сумма после перевода в USD или EUR, для RUB - сумма не меняется</summary>
        public double ConversionValute
        {
            get { return сonversionValute; }
            set { Set(ref сonversionValute, value); }
        }

        /// <summary>Счет для зачисления</summary>
        public string AnotherAccount
        {
            get { return anotherAccount; }
            set { Set(ref anotherAccount, value); }
        }

        /// <summary>Сумма для зачисления на счет</summary>
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
                    case Cash.USD: ConversionValute = TransferAmount * GetValute.GetDataCurrentValute(Currency).Item3;
                        break;
                    case Cash.EUR: ConversionValute = TransferAmount * GetValute.GetDataCurrentValute(Currency).Item3;
                        break;
                    default:
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
                    AccountBalance = Client.DepositAccount.BalanceRUB_Account;
                    AnotherAccount = "Основной счет";
                    Visibility = "Hidden";
                    XmlBalance = "Баланс, руб";
                }
                else
                {
                    AccountBalance = Client.NonDepositAccount.BalanceRUB_Account;
                    AnotherAccount = "Депозитный счет";
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

        /// <summary>Свойство видимости для суммы на счете, активируется при первом загрузке страницы до момента выбора каког-либо счета</summary>
        public string VisibilityAccountBalance
        {
            get { return visibilityAccountBalance; }
            set { Set(ref visibilityAccountBalance, value); }
        }

        /// <summary>Текстовое представление клиентских аккаунтов</summary>
        public List<string> ClientAccount
        {
            get{ return Client.DepositAccount.DepositNotExist ? new List<string> { "Основной счет" } : clientAccount; }
            set { Set(ref clientAccount, value); }
        }

        /// <summary>Количество средств на выбранном счете</summary>
        public double AccountBalance
        {
            get { return accountBalance; }
            set{ Set(ref accountBalance, value); }
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
                        AccountBalance = Client.NonDepositAccount.BalanceRUB_Account;
                        break;
                    case Cash.USD:
                        AccountBalance = Client.NonDepositAccount.BalanceUSD_Account;
                        break;
                    case Cash.EUR:
                        AccountBalance = Client.NonDepositAccount.BalanceEURO_Account;
                        break;
                }
            }
        }

        #region Команда перевода среств клиента между счетами
        public ICommand TransferAmountCommand { get; }

        private bool CanTransferAmountCommandExecute(object parametr)
        {
            if (TransferAmount > AccountBalance || TransferAmount <= 0 || Client.DepositAccount.DepositNotExist || SelectedAccount == null) return false;
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
                        break;
                    case Cash.USD:
                        Client.NonDepositAccount.BalanceUSD_Account -= TransferAmount;
                        break;
                    case Cash.EUR:
                        Client.NonDepositAccount.BalanceEURO_Account -= TransferAmount;
                        break;
                }
                Client.DepositAccount.BalanceRUB_Account += ConversionValute;
                NotifyTransferBetweenAccounts?.Invoke($"{DateTime.Now} {Client.Id} {Client.LastName} {Client.Name} {Client.Patronymic}" +
                                                      $" перевод {TransferAmount} с {Currency} --> Депозитный");
            }
            else
            {
                Client.NonDepositAccount.BalanceRUB_Account += ConversionValute;
                Client.DepositAccount.BalanceRUB_Account -= TransferAmount;
                NotifyTransferBetweenAccounts?.Invoke($"{DateTime.Now} {Client.Id} {Client.LastName} {Client.Name} {Client.Patronymic}" +
                                                      $" перевод {TransferAmount} с Депозитного --> Основной");
            }
            Application.Current.Windows[1].Close();
        } 
        #endregion

        public TransferBetweenAccountsViewModel()
        {
            TransferAmountCommand = new RelayCommand(OnTransferAmountCommandExecuted, CanTransferAmountCommandExecute);
        }

    }
}
