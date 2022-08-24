using System;
using ДЗ_11.Models.AbstractModels;

namespace ДЗ_11.Models
{
    internal class NonDepositAccount : BaseAccount
    {    
        private double balanceRUB_Account;
        private double balanceUSD_Account;
        private double balanceEURO_Account;
       
        public override DateTime DateOfCreation { get; set; }
        public override DateTime DateOfClose { get; set; }

        public override double BalanceRUB_Account
        {
            get => balanceRUB_Account;
            set => Set(ref balanceRUB_Account, value);
        }
        public double BalanceUSD_Account
        {
            get => balanceUSD_Account;
            set => Set(ref balanceUSD_Account, value);
        }
        public double BalanceEURO_Account
        {
            get => balanceEURO_Account;
            set => Set(ref balanceEURO_Account, value);
        }
        
        public NonDepositAccount() : base()
        {
            balanceRUB_Account = 0;
            balanceUSD_Account = 0;
            balanceEURO_Account = 0;
        }

        public NonDepositAccount(DateTime dateOfCreation, double balanceRUB_Account, double balanceUSD_Account, double balanceEURO_Account, DateTime dateOfClose)
        {
            DateOfCreation = dateOfCreation;
            this.balanceRUB_Account = balanceRUB_Account;
            this.balanceUSD_Account = balanceUSD_Account;
            this.balanceEURO_Account= balanceEURO_Account;
            DateOfClose = dateOfClose;
        }

        public override string ToString()
        {
            return $"Обычный счет {DateOfCreation} {BalanceRUB_Account} {BalanceUSD_Account} {BalanceEURO_Account} {DateOfClose}";
        }
    }
}
